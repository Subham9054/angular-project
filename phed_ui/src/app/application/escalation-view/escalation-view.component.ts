import { Component } from '@angular/core';
import { AuthService } from 'src/app/auth.service';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import Swal from 'sweetalert2';
import * as CryptoJS from 'crypto-js';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-escalation-view',
  templateUrl: './escalation-view.component.html',
  styleUrls: ['./escalation-view.component.scss']
})
export class EscalationViewComponent {
  secretKey: any = environment.apiHashingKey;
  // Filter close btn
  isDropdownOpen = false;
  openDropdown() {
    this.isDropdownOpen = true;
  }


  closeDropdown() {
    this.isDropdownOpen = false;
  }

  // Search filter
  isPanelOpen = false;

  togglePanel() {
    this.isPanelOpen = !this.isPanelOpen;
  }
  escalations: any[] = [];
  categories: any[] = [];
  subcategories: any[] = [];
  selectedEscalations: any = [];

  formData: any = {
    ddlComplaintCategory: '0',
    ddlSubCategory: '0',
  };

  constructor(private http: HttpClient, private authService: AuthService, private router: Router) { }

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
    const catid = categoryId;
    const subcatid = subCategoryId;
    this.authService.viewEscalationeye(catid, subcatid).subscribe(
      data => {
        this.selectedEscalations = data;
      },
      error => {
        console.error('Error fetching escalations:', error);
      }
    );
  }





  Updateview(categoryId: string, subCategoryId: string, escalationlevelId: string) {
    // const secretKey = 'your-secret-key';
    const catid = categoryId;
    const subcatid = subCategoryId;
    const esclid = escalationlevelId;
    // Encrypting the parameters
    const encryptedCategory = CryptoJS.AES.encrypt(categoryId, this.secretKey).toString();
    const encryptedSubCategory = CryptoJS.AES.encrypt(subCategoryId, this.secretKey).toString();
    const encryptedComplaintId = CryptoJS.AES.encrypt(escalationlevelId, this.secretKey).toString();
    const numberRepresentation = parseInt(CryptoJS.MD5(encryptedComplaintId).toString(CryptoJS.enc.Hex).slice(0, 8), 16);

    this.router.navigate(['/application/escalation/add'], {
      queryParams: {
        catid: encryptedCategory,
        subcatid: encryptedSubCategory,
        esclid: encryptedComplaintId

      }
    });

  }

}


