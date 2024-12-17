import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/auth.service';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-whatnew-view',
  templateUrl: './whatnew-view.component.html',
  styleUrls: ['./whatnew-view.component.scss']
})
export class WhatnewViewComponent implements OnInit {
  // Filter close btn
  isDropdownOpen = false;
  openDropdown() {
    this.isDropdownOpen = true;
  }

  closeDropdown() {
    this.isDropdownOpen = false;
  }

  whatIsNewDetails: any[] = [];
  selectedWhatIsNews: any = null;
  noRecordsFound = false;

  constructor(private authService: AuthService, private router: Router) {}

  ngOnInit(): void {
    debugger;
    this.fetchNewsEvents();
  }

  // Method to fetch event details from the service
  fetchNewsEvents() {
    debugger;
    this.authService.getWhatIsNews().subscribe({
      next: (response) => {
        if (response && response.success) {
          // Map the response data to include the full image URL
          this.whatIsNewDetails = response.data.map((whatNew: any) => ({
            ...whatNew,
            document: `http://localhost:5234${whatNew.whatnews}`            
          }));
          this.noRecordsFound = this.whatIsNewDetails.length === 0;
          if (this.noRecordsFound) {
            Swal.fire({
              icon: 'info',
              title: 'No What is New Found',
              text: 'There are no what is news to display.',
            });
          }
        } else {
          this.noRecordsFound = true;
          Swal.fire({
            icon: 'error',
            title: 'Failed to Fetch What is New.',
            text: 'Could not retrieve waht is new details.',
          });
        }
      },
      error: (error) => {
        console.error('Error fetching what is new details.', error);
        this.noRecordsFound = true;
        Swal.fire({
          icon: 'error',
          title: 'Error',
          text: 'An error occurred while fetching what is new.',
        });
      }
    });
  }

  // downloadDocument(id: number) {
  //   debugger;
  //   this.authService.downloadDocument(id).subscribe({
  //     next: (response: Blob) => {
  //       const blob = new Blob([response], { type: response.type });
  //       const url = window.URL.createObjectURL(blob);
  //       const anchor = document.createElement('a');
  //       anchor.href = url;
  //       anchor.download = `document_${id}.pdf`; // You can dynamically set the filename
  //       anchor.click();
  //       window.URL.revokeObjectURL(url);
  //       Swal.fire('Success', 'Document downloaded successfully!', 'success');
  //     },
  //     error: (error) => {
  //       console.error('Error downloading document:', error);
  //       Swal.fire('Error', 'Unable to download the document. Please try again.', 'error');
  //     }
  //   });
  // }

  // downloadDocument(id: number) {
  //   this.authService.downloadDocument(id).subscribe({
  //       next: (response: Blob) => {
  //           const blob = new Blob([response], { type: response.type });
  //           const url = window.URL.createObjectURL(blob);
  //           const anchor = document.createElement('a');
  //           anchor.href = url;
  //           anchor.download = `document_${id}.pdf`; // You can dynamically set the filename
  //           anchor.click();
  //           window.URL.revokeObjectURL(url); // Revoke the object URL to release memory
  //           Swal.fire('Success', 'Document downloaded successfully!', 'success');
  //       },
  //       error: (error) => {
  //           console.error('Error downloading document:', error);
  //           Swal.fire('Error', 'Unable to download the document. Please try again.', 'error');
  //       }
  //   });
  // }


  // Method to handle the Modal View action
  onView(whatIsNews: any): void {
    this.selectedWhatIsNews = whatIsNews;
  }

  // Method to handle the edit action
  editWhatIsNewDetails(id: number) {
    // Navigate to the edit page with whatIsNewId as a route parameter
    this.router.navigate([`/application/whatsnew/${id}`]);
  }

  // Method to handle the delete action
  deleteWhatIsNewDetails(id: number): void {
    Swal.fire({
      title: 'Are you sure?',
      text: 'Do you want to delete the what is new details?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Yes, delete it!',
      cancelButtonText: 'No, keep it',
    }).then((result) => {
      if (result.isConfirmed) {
        this.authService.deleteWhatIsNew(id).subscribe({
          next: () => {
            // Remove the deleted what's new details from the array
            this.whatIsNewDetails = this.whatIsNewDetails.filter((whatNew) => whatNew.whatIsNewId !== id);
            this.noRecordsFound = this.whatIsNewDetails.length === 0; // Check if no records remain
            Swal.fire('Deleted!', 'Your waht is new detail has been deleted.', 'success');
          },
          error: (error) => {
            console.error('Error deleting what is new details:', error);
            Swal.fire('Error', 'Unable to delete the what is new details. Please try again.', 'error');
          },
        });
      }
    });
  }
}