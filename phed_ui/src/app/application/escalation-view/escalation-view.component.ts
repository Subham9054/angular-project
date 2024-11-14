import { Component } from '@angular/core';
import { AuthService } from 'src/app/auth.service';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-escalation-view',
  templateUrl: './escalation-view.component.html',
  styleUrls: ['./escalation-view.component.scss']
})
export class EscalationViewComponent {
  isPanelOpen = false; // Start with the panel open

  togglePanel() {
    this.isPanelOpen = !this.isPanelOpen; // Toggle the panel state
  }
  escalations: any[] = [];
  categories: any[] = [];
  subcategories: any[] = [];
  selectedEscalations: any = []; // Property to store selected escalation details

  formData: any = { 
    ddlComplaintCategory: '0',
    ddlSubCategory: '0',
  };

  constructor(private http: HttpClient, private authService: AuthService, private router: Router) {}

  ngOnInit() {
    this.getCategories();
    this.onSearch();
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

  // onSearch() {
  //   const catdropdown = this.formData.ddlComplaintCategory;
  //   const subcatdropdown = this.formData.ddlSubCategory;

  //   if (catdropdown !== "0" && subcatdropdown === "0") {
  //     alert("Please select subcategory.");
  //     return;
  //   }
    
  //   this.authService.viewEscalation(catdropdown, subcatdropdown).subscribe(
  //     data => {
  //       this.escalations = data;
  //     },
  //     error => {
  //       alert(error.error);
  //       return false;
  //       //console.error('Error fetching escalations:', error);
  //     }
  //   );
  // }
  onSearch() {
    const catdropdown = this.formData.ddlComplaintCategory;
    const subcatdropdown = this.formData.ddlSubCategory;
  
    if (catdropdown !== "0" && subcatdropdown === "0") {
      Swal.fire({
        icon: 'warning',
        title: 'Oops...',
        text: 'Please select a subcategory.',
      });
      return;
    }
  
    this.authService.viewEscalation(catdropdown, subcatdropdown).subscribe(
      data => {
        this.escalations = data;
        // Swal.fire({
        //   icon: 'success',
        //   title: 'Success!',
        //   text: 'Escalations fetched successfully.',
        // });
      },
      error => {
        // Here, we can handle different types of errors more gracefully
        Swal.fire({
          icon: 'error',
          title: 'Error!',
          text: error.error ? error.error : 'An unexpected error occurred.',
        });
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

  // Method to set the selected escalation details for the modal
  viewEscalationDetails(categoryId: string, subCategoryId: string): void {
    // alert(categoryId);
    // alert(subCategoryId);
    const catid=categoryId;
    const subcatid=subCategoryId;
    this.authService.viewEscalationeye(catid, subcatid).subscribe(
      data => {
        this.selectedEscalations = data;
      },
      error => {
        console.error('Error fetching escalations:', error);
      }
    );
  }
  Updateview(categoryId: string, subCategoryId: string,escalationlevelId:string){
    // alert(categoryId);
    // alert(subCategoryId);
    // alert(escalationlevelId);
    const catid=categoryId;
    const subcatid=subCategoryId;
    const esclid=escalationlevelId;
    this.router.navigate(['/application/escalation/add'], {
      queryParams: {
        catid: categoryId,
        subcatid: subCategoryId,
        esclid: escalationlevelId
      }
    });
   
}

}
