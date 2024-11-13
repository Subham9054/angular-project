import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/auth.service';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-mngbanner-view',
  templateUrl: './mngbanner-view.component.html',
  styleUrls: ['./mngbanner-view.component.scss']
})
export class MngbannerViewComponent {
  isPanelOpen = true; // Start with the panel open
  banners: any[] = [];
  noRecordsFound = false;

  constructor(private authService: AuthService, private router: Router) {}

  ngOnInit(): void {
    debugger;
    this.fetchBanners();
  }

  togglePanel() {
    this.isPanelOpen = !this.isPanelOpen; // Toggle the panel state
  }

  // Method to fetch banners from the service
  fetchBanners() {
    debugger;
    this.authService.GetBanners().subscribe({
      next: (response) => {
        if (response && response.success) {
          // Map the response data to include the full image URL
          this.banners = response.data.map((banner: any) => ({
            ...banner,
            bannerImage: `http://localhost:5097${banner.bannerImage}`
          }));
          this.noRecordsFound = this.banners.length === 0;

          if (this.noRecordsFound) {
            Swal.fire({
              icon: 'info',
              title: 'No Banners Found',
              text: 'There are no banners to display.',
            });
          }
        } else {
          this.noRecordsFound = true;
          Swal.fire({
            icon: 'error',
            title: 'Failed to Fetch Banners',
            text: 'Could not retrieve banner data.',
          });
        }
      },
      error: (error) => {
        console.error('Error fetching banners', error);
        this.noRecordsFound = true;
        Swal.fire({
          icon: 'error',
          title: 'Error',
          text: 'An error occurred while fetching banners.',
        });
      }
    });
  }


  // Method to handle the edit action
  editBannerDetails(id: number) {
    this.router.navigate(['/application/mngbanner'], { queryParams: { pageId: id } });
  }

  // Method to handle the delete action
  deleteBannerDetails(id: number): void {
    Swal.fire({
      title: 'Are you sure?',
      text: 'Do you want to delete this banner?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Yes, delete it!',
      cancelButtonText: 'No, keep it'
    }).then((result) => {
      if (result.isConfirmed) {
        this.authService.DeleteBanner(id).subscribe({
          next: () => {
            this.banners = this.banners.filter(banner => banner.bannerId !== id);
            this.noRecordsFound = this.banners.length === 0; // Update if no records remain
            Swal.fire('Deleted!', 'Your banner has been deleted.', 'success');
          },
          error: (error) => {
            console.error('Error deleting banner:', error);
            Swal.fire('Error', 'Unable to delete banner', 'error');
          }
        });
      }
    });
  }
}