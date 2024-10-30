import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/auth.service';
import * as CryptoJS from 'crypto-js';


@Component({
  selector: 'app-complaintcategory-view',
  templateUrl: './complaintcategory-view.component.html',
  styleUrls: ['./complaintcategory-view.component.scss']
})
export class ComplaintcategoryViewComponent {
  complaints: any[] = [];

  constructor(private authService: AuthService, private router: Router) {}

  ngOnInit(): void {
    this.authService.getAllComplaints().subscribe(
      (data) => {
        this.complaints = data;  // Set the complaints data if the request succeeds
      },
      (error) => {
        console.error('Error fetching complaints:', error);  // Handle the error case
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

    this.router.navigate(['/complaintcategory'], {
      queryParams: {
        VCH_CATEGORY: encryptedCategory,
        NVCH_CATEGORY: encryptedSubCategory,
        complaintId: complaint.inT_CATEGORY_ID
      }
    });
  }

  deleteComplaint(id: number) {
    debugger;
    if (id) {
      this.authService.delete(id).subscribe(
        response => {
          // Handle successful login
          console.log('complaint category delete successful', response);
          alert(" complaint category delete successful");
          this.router.navigate(['/complaintcategory-view']);
          this.authService.getAllComplaints().subscribe();
          window.location.reload();
        },
        error => {
          // Handle error
          console.error('complaint category delete failed', error);
          alert("complaint category delete failed");
        }
      );
    }
  }
}

