import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/auth.service';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';

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
  selectedEvents: any = null;
  noRecordsFound = false;

  constructor(private authService: AuthService, private router: Router) {}

  ngOnInit(): void {
    debugger;
    this.fetchNewsEvents();
  }

  // Method to fetch event details from the service
  fetchNewsEvents() {
    debugger;
    this.authService.getEvents().subscribe({
      next: (response) => {
        if (response && response.success) {
          // Map the response data to include the full image URL
          this.eventdetails = response.data.map((newsEvent: any) => ({
            ...newsEvent,
            thumbImage: `http://localhost:5097${newsEvent.thumbnail}`,
            featureImage: `http://localhost:5097${newsEvent.featureImage}`
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
  onView(eventImage: any): void {
    this.selectedEvents = eventImage;
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
      text: 'Do you want to delete the event details?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Yes, delete it!',
      cancelButtonText: 'No, keep it',
    }).then((result) => {
      if (result.isConfirmed) {
        this.authService.deleteEvent(id).subscribe({
          next: () => {
            // Remove the deleted event details from the array
            this.eventdetails = this.eventdetails.filter((event) => event.eventId !== id);
            this.noRecordsFound = this.eventdetails.length === 0; // Check if no records remain
            Swal.fire('Deleted!', 'Your event has been deleted.', 'success');
          },
          error: (error) => {
            console.error('Error deleting event details:', error);
            Swal.fire('Error', 'Unable to delete the event details. Please try again.', 'error');
          },
        });
      }
    });
  }
}