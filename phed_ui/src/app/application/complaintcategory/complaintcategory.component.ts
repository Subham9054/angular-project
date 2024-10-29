import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthService } from 'src/app/auth.service';
import * as CryptoJS from 'crypto-js';

@Component({
  selector: 'app-complaintcategory',
  templateUrl: './complaintcategory.component.html',
  styleUrls: ['./complaintcategory.component.scss']
})
export class ComplaintcategoryComponent implements OnInit {
  VCH_CATEGORY: string = '';
  NVCH_CATEGORY: string = '';
  isUpdateMode: boolean = false;
  complaintId: number | null = null;

  constructor(
    private authService: AuthService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit() {
    // Check if the token has expired
    if (this.authService.isTokenExpired()) {
      alert("Session expired. Please login again.");
      this.authService.logout();  // Clear session storage and navigate to login
      this.router.navigate(['/login']);  // Redirect to the login page
      return; // Exit if the session is expired
    }

    // Retrieve query parameters
    this.route.queryParams.subscribe(params => {
      if (params['complaintId']) {
        this.isUpdateMode = true;
        this.complaintId = +params['complaintId']; // Convert to number

        // Decrypt the categories if they are passed as encrypted
        const secretKey = 'your-secret-key'; // Use the same key for decryption
        this.VCH_CATEGORY = this.decryptData(params['VCH_CATEGORY'], secretKey);
        this.NVCH_CATEGORY = this.decryptData(params['NVCH_CATEGORY'], secretKey);
      }
    });
  }

  // Method to decrypt data
  decryptData(encryptedData: string, secretKey: string): string {
    const bytes = CryptoJS.AES.decrypt(encryptedData, secretKey);
    return bytes.toString(CryptoJS.enc.Utf8);
  }

  submitComplaint() {
    if (this.VCH_CATEGORY && this.NVCH_CATEGORY) {
      const complaintData = {
        VCH_CATEGORY: this.VCH_CATEGORY,
        NVCH_CATEGORY: this.NVCH_CATEGORY
      };
      this.authService.submitComplaint(complaintData).subscribe(
        (response) => {
          alert('Complaint category submitted successfully!');
          this.resetForm();
        },
        (error) => {
          console.error('Error submitting complaint', error);
          alert('Failed to submit the complaint category.');
        }
      );
    } else {
      alert('Please fill in all required fields.');
    }
  }

  updateComplaint() {
    if (this.complaintId && this.VCH_CATEGORY && this.NVCH_CATEGORY) {
      const secretKey = 'your-secret-key'; // Your secret key for encryption

      // Encrypt the categories
      const encryptedVCH_CATEGORY = CryptoJS.AES.encrypt(this.VCH_CATEGORY, secretKey).toString();
      const encryptedNVCH_CATEGORY = CryptoJS.AES.encrypt(this.NVCH_CATEGORY, secretKey).toString();

      const complaintData = {
        VCH_CATEGORY: this.VCH_CATEGORY, // Use encrypted value
        NVCH_CATEGORY: this.NVCH_CATEGORY // Use encrypted value
      };

      // Call the updateComplaint method with the complaintId
      this.authService.updateComplaint(this.complaintId, complaintData).subscribe(
        (response) => {
          alert('Complaint category updated successfully!');
          this.router.navigate(['/complaintcategory-view']);
        },
        (error) => {
          console.error('Error updating complaint', error);
          alert('Failed to update the complaint category.');
        }
      );
    } else {
      alert('Please fill in all required fields.');
    }
  }

  viewComplaints() {
    this.router.navigate(['/complaint-view']);
    this.authService.getAllComplaints().subscribe(); // Handle response if needed
  }

  resetForm() {
    this.VCH_CATEGORY = '';
    this.NVCH_CATEGORY = '';
    this.isUpdateMode = false;
    this.complaintId = null;
  }
}
