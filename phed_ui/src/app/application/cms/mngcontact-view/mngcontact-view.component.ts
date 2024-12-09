import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/auth.service';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-mngcontact-view',
  templateUrl: './mngcontact-view.component.html',
  styleUrls: ['./mngcontact-view.component.scss']
})
export class MngcontactViewComponent implements OnInit {
  contactdetails: any[] = [];
  noRecordsFound = false;

  constructor(private authService: AuthService, private router: Router) {}

  ngOnInit(): void {
    debugger;
    this.fetchContactDetails();
  }

  // Method to fetch contact details from the service
  fetchContactDetails() {
    debugger;
    this.authService.getContacts().subscribe({
      next: (response) => {
        if (response && response.success) {
          // Map the response data to include the full image URL
          this.contactdetails = response.data.map((contacts: any) => ({
            ...contacts
          }));
          this.noRecordsFound = this.contactdetails.length === 0;
          if (this.noRecordsFound) {
            Swal.fire({
              icon: 'info',
              title: 'No Contact Details Found',
              text: 'There are no contact details to display.',
            });
          }
        } else {
          this.noRecordsFound = true;
          Swal.fire({
            icon: 'error',
            title: 'Failed to Fetch Contact Details.',
            text: 'Could not retrieve contact details.',
          });
        }
      },
      error: (error) => {
        console.error('Error fetching contact details.', error);
        this.noRecordsFound = true;
        Swal.fire({
          icon: 'error',
          title: 'Error',
          text: 'An error occurred while fetching contact details.',
        });
      }
    });
  }

  // Method to handle the edit action
  editContactDetails(id: number) {
    // Navigate to the edit page with galleryId as a route parameter
    this.router.navigate([`/application/mngcontact/${id}`]);
  }

  // Method to handle the delete action
  deleteContactDetails(id: number): void {
    Swal.fire({
      title: 'Are you sure?',
      text: 'Do you want to delete the contact details?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Yes, delete it!',
      cancelButtonText: 'No, keep it',
    }).then((result) => {
      if (result.isConfirmed) {
        this.authService.deleteContact(id).subscribe({
          next: () => {
            // Remove the deleted event details from the array
            this.contactdetails = this.contactdetails.filter((contact) => contact.contactId !== id);
            this.noRecordsFound = this.contactdetails.length === 0; // Check if no records remain
            Swal.fire('Deleted!', 'Your contact details has been deleted.', 'success');
          },
          error: (error) => {
            console.error('Error deleting contact details:', error);
            Swal.fire('Error', 'Unable to delete the contact details. Please try again.', 'error');
          },
        });
      }
    });
  }
}
