import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/auth.service';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';

declare let $: any;
@Component({
  selector: 'app-newsevent-view',
  templateUrl: './newsevent-view.component.html',
  styleUrls: ['./newsevent-view.component.scss']
})
export class NewseventViewComponent implements OnInit {
  // Filter close btn
  isDropdownOpen = false;

  openDropdown() {
    this.isDropdownOpen = true;
  }

  closeDropdown() {
    this.isDropdownOpen = false;
  }

  eventdetails: any[] = [];
  selectedGallery: any = null;
  noRecordsFound = false;

  constructor(private authService: AuthService, private router: Router) {}

  ngOnInit(): void {
    debugger;
    this.fetchNewsEvents();
  }

  // Method to fetch banners from the service
  fetchNewsEvents() {
    debugger;
    this.authService.getEvents().subscribe({
      next: (response) => {
        if (response && response.success) {
          // Map the response data to include the full image URL
          this.eventdetails = response.data.map((newsEvent: any) => ({
            ...newsEvent,
            thumbImage: `http://localhost:5097${newsEvent.thumbnail}`
          }));
          this.noRecordsFound = this.eventdetails.length === 0;
          if (this.noRecordsFound) {
            Swal.fire({
              icon: 'info',
              title: 'No News & Events Found',
              text: 'There are no News & Events to display.',
            });
          }
        } else {
          this.noRecordsFound = true;
          Swal.fire({
            icon: 'error',
            title: 'Failed to Fetch News & Events.',
            text: 'Could not retrieve news & events details.',
          });
        }
      },
      error: (error) => {
        console.error('Error fetching news & events', error);
        this.noRecordsFound = true;
        Swal.fire({
          icon: 'error',
          title: 'Error',
          text: 'An error occurred while fetching news & events.',
        });
      }
    });
  }

  // Method to handle the Modal View action
  onView(galy: any): void {
    this.selectedGallery = galy;
  }

  // Method to handle the edit action
  editEventDetails(id: number) {
    // Navigate to the edit page with galleryId as a route parameter
    this.router.navigate([`/application/newsevent/${id}`]);
  }

  // Method to handle the delete action
  deleteEventDetails(id: number): void {
    Swal.fire({
      title: 'Are you sure?',
      text: 'Do you want to delete the gallery?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Yes, delete it!',
      cancelButtonText: 'No, keep it',
    }).then((result) => {
      if (result.isConfirmed) {
        this.authService.deleteGallery(id).subscribe({
          next: () => {
            // Remove the deleted gallery from the array
            this.eventdetails = this.eventdetails.filter((gallery) => gallery.galleryId !== id);
            this.noRecordsFound = this.eventdetails.length === 0; // Check if no records remain
            Swal.fire('Deleted!', 'Your gallery has been deleted.', 'success');
          },
          error: (error) => {
            console.error('Error deleting gallery:', error);
            Swal.fire('Error', 'Unable to delete the gallery. Please try again.', 'error');
          },
        });
      }
    });
  }
}
