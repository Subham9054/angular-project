import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/auth.service';
import { LoadingService } from 'src/app/loading.service'; // Correct import path
import * as CryptoJS from 'crypto-js';

@Component({
  selector: 'app-complaintcategory-view',
  templateUrl: './complaintcategory-view.component.html',
  styleUrls: ['./complaintcategory-view.component.scss']
})
export class ComplaintcategoryViewComponent implements OnInit {
  complaints: any[] = [];

  constructor(
    private authService: AuthService,
    private router: Router,
    private loadingService: LoadingService  // Inject LoadingService
  ) {}

  ngOnInit(): void {
    this.loadingService.startLoading();  // Show loader when API request starts

    this.authService.getAllComplaints().subscribe(
      (data) => {
        this.complaints = data;
        this.loadingService.stopLoading();  // Hide loader after data is received
      },
      (error) => {
        console.error('Error fetching complaints:', error);
        this.loadingService.stopLoading();  // Hide loader even in case of error
      }
    );
  }

  updateComplaint(complaint: any) {
    const secretKey = 'your-secret-key';
    // Encrypting the parameters
    const encryptedCategory = CryptoJS.AES.encrypt(complaint.vcH_CATEGORY, secretKey).toString();
    const encryptedSubCategory = CryptoJS.AES.encrypt(complaint.nvcH_CATEGORY, secretKey).toString();
    const encryptedComplaintId = CryptoJS.AES.encrypt(complaint.inT_CATEGORY_ID.toString(), secretKey).toString();
    const numberRepresentation = parseInt(CryptoJS.MD5(encryptedComplaintId).toString(CryptoJS.enc.Hex).slice(0, 8), 16);

    this.router.navigate(['/application/complaintcategory'], {
      queryParams: {
        VCH_CATEGORY: encryptedCategory,
        NVCH_CATEGORY: encryptedSubCategory,
        complaintId: complaint.inT_CATEGORY_ID
      }
    });
  }

  deleteComplaint(id: number) {
    if (id) {
      this.loadingService.startLoading();  // Show loader before delete request

      this.authService.delete(id).subscribe(
        response => {
          console.log('complaint category delete successful', response);
          alert("complaint category delete successful");
          this.router.navigate(['/complaintcategory-view']);
          this.authService.getAllComplaints().subscribe();
          window.location.reload();
          this.loadingService.stopLoading();  // Hide loader after delete request
        },
        error => {
          console.error('complaint category delete failed', error);
          alert("complaint category delete failed");
          this.loadingService.stopLoading();  // Hide loader after error
        }
      );
    }
  }
}
