import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/auth.service';
import { Router } from '@angular/router';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-pagecontent-view',
  templateUrl: './pagecontent-view.component.html',
  styleUrls: ['./pagecontent-view.component.scss']
})
export class PagecontentViewComponent implements OnInit {
  cmsBaseURL = 'https://localhost:7197/gateway'; // Declare cmsBaseURL in the component
  isDropdownOpen = false;  // Filter close btn / Dropdown control
  pageContentDetails: any[] = [];
  selectedPageContents: any = null;
  noRecordsFound = false;

  constructor(private authService: AuthService, private router: Router) {}

  ngOnInit(): void {    
    this.fetchPageContents();
  }

  openDropdown() {
    this.isDropdownOpen = true;
  }

  closeDropdown() {
    this.isDropdownOpen = false;
  } 
  
  // Method to fetch page content details from the service
  fetchPageContents() {   
    this.authService.getPageContents().subscribe({
      next: (response) => {
        if (response && response.success) {
          // Map the response data to include the full image URL
          this.pageContentDetails = response.data.map((contentDetails: any) => ({
            ...contentDetails,
            featureImage: `${this.cmsBaseURL}${contentDetails.featureImage}`            
            // featureImage: `http://localhost:5234${contentDetails.featureImage}`
            // featureImage: `http://localhost:5097${contentDetails.featureImage}`
          }));
          this.noRecordsFound = this.pageContentDetails.length === 0;
          if (this.noRecordsFound) {
            Swal.fire({
              icon: 'info',
              title: 'No Page Content Found',
              text: 'There are no page contents to display.',
            });
          }
        } else {
          this.noRecordsFound = true;
          Swal.fire({
            icon: 'error',
            title: 'Failed to Fetch Page Contents.',
            text: 'Could not retrieve page content details.',
          });
        }        
      },
      error: (error) => {
        console.error('Error fetching page content details.', error);
        this.noRecordsFound = true;        
        Swal.fire({
          icon: 'error',
          title: 'Error',
          text: 'An error occurred while fetching page content details.',
        });
      }
    });
  }

  // Method to handle the Modal View action (View specific page content)
  onView(pageContents: any): void {
    this.selectedPageContents = pageContents;
  }
  
  // Method to handle the edit action
  editPageContentDetails(id: number) {
    // Navigate to the edit page with whatIsNewId as a route parameter
    this.router.navigate([`/application/pagecontent/${id}`]);
  }
  
  // Method to handle the delete action
  deletePageContentDetails(id: number): void {
    Swal.fire({
      title: 'Are you sure?',
      text: 'Do you want to delete the page content details?',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Yes, delete it!',
      cancelButtonText: 'No, keep it',
    }).then((result) => {
      if (result.isConfirmed) {
        this.authService.deletePageContent(id).subscribe({
          next: () => {
            // Remove the deleted page content details from the array
            this.pageContentDetails = this.pageContentDetails.filter((pageContent) => pageContent.contentId !== id);
            this.noRecordsFound = this.pageContentDetails.length === 0; // Check if no records remain
            Swal.fire('Deleted!', 'Your Page Content details has been deleted.', 'success');
          },
          error: (error) => {
            console.error('Error deleting event details:', error);
            Swal.fire('Error', 'Unable to delete the page content details. Please try again.', 'error');
          },
        });
      }
    });
  }
}
