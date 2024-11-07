import { Component } from '@angular/core';
import { AuthService } from 'src/app/auth.service';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-escalation-view',
  templateUrl: './escalation-view.component.html',
  styleUrls: ['./escalation-view.component.scss']
})
export class EscalationViewComponent {



  categories: any[] = [];
  subcategories: any[] = [];

  formData: any = { 
    ddlComplaintCategory: '0',
    ddlSubCategory: '0',
  };

  constructor(private http: HttpClient, private authService: AuthService, private router: Router) {}
  ngOnInit() {
    this.getCategories();
  }
  getCategories() {
    this.authService.getCategories().subscribe(
      response => {
        this.categories = response;
        console.log(this.categories);
      },
      error => {
        console.error('Error fetching categories', error);
      }
    );
  }

  onCategoryChange(event: any) {
    const catid = parseInt(event.target.value, 10);
    if (!isNaN(catid)) {
      this.authService.getsubcategories(catid).subscribe(
        response => {
          this.subcategories = response;
          console.log(this.subcategories);
        },
        error => {
          console.error('Error fetching subcategories', error);
        }
      );
    } else {
      console.error('Invalid category ID');
    }
  }
}
