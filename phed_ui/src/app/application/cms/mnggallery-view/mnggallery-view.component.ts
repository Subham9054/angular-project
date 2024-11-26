import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/auth.service';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-mnggallery-view',
  templateUrl: './mnggallery-view.component.html',
  styleUrls: ['./mnggallery-view.component.scss']
})
export class MnggalleryViewComponent implements OnInit {
  // Filter close btn
  isDropdownOpen = false;

  openDropdown() {
    this.isDropdownOpen = true;
  }

  closeDropdown() {
    this.isDropdownOpen = false;
  }

  galleries: any[] = [];
  noRecordsFound = false;

  constructor(private authService: AuthService, private router: Router) {}

  ngOnInit(): void {
    debugger;
    this.fetchGalleries();
  }

  // Method to fetch banners from the service
  fetchGalleries() {
    debugger;
    this.authService.getGallery().subscribe({
      next: (response) => {
        if (response && response.success) {
          // Map the response data to include the full image URL
          this.galleries = response.data.map((gallery: any) => ({
            ...gallery,
            thumbImage: `http://localhost:5097${gallery.thumbnail}`
          }));
          this.noRecordsFound = this.galleries.length === 0;
          if (this.noRecordsFound) {
            Swal.fire({
              icon: 'info',
              title: 'No Galleries Found',
              text: 'There are no galleries to display.',
            });
          }
        } else {
          this.noRecordsFound = true;
          Swal.fire({
            icon: 'error',
            title: 'Failed to Fetch Galleries',
            text: 'Could not retrieve gallery details.',
          });
        }
      },
      error: (error) => {
        console.error('Error fetching galleries', error);
        this.noRecordsFound = true;
        Swal.fire({
          icon: 'error',
          title: 'Error',
          text: 'An error occurred while fetching galleries.',
        });
      }
    });
  }

  // Method to handle the edit action
  editGalleryDetails(id: number) {
    this.router.navigate([`/application/mnggallery/${id}`]);
  }

  // Method to handle the delete action
  deleteGalleryDetails(id: number): void {
    Swal.fire({
      title: 'Are you sure?',
      text: 'Do you want to delete the gallery?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Yes, delete it!',
      cancelButtonText: 'No, keep it'
    }).then((result) => {
      if (result.isConfirmed) {
        this.authService.deleteGallery(id).subscribe({
          next: () => {
            this.galleries = this.galleries.filter(gallery => gallery.galleryId !== id);
            this.noRecordsFound = this.galleries.length === 0; // Update if no records remain
            Swal.fire('Deleted!', 'Your gallery has been deleted.', 'success');
          },
          error: (error) => {
            console.error('Error deleting gallery:', error);
            Swal.fire('Error', 'Unable to delete gallery', 'error');
          }
        });
      }
    });
  }
}
