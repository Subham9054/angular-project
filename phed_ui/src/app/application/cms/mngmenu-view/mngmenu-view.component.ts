import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/auth.service';
import Swal from 'sweetalert2';
import { Router } from '@angular/router';

interface MenuItem {
  pageId: number;
  pageNameEng: string;
  pageNameHin: string;
  parentNameEng: string;
  parentNameHin: string;
  url: string;
  isSubMenu: boolean;
}

interface MenuResponse {
  success: boolean;
  data: MenuItem[];
}

@Component({
  selector: 'app-mngmenu-view',
  templateUrl: './mngmenu-view.component.html',
  styleUrls: ['./mngmenu-view.component.scss']
})
export class MngmenuViewComponent implements OnInit {
  menuData: MenuItem[] = [];
  noRecordsFound: boolean = false; // Flag to show "No Page Links Found"

  constructor(private authService: AuthService, private router: Router) {} // Inject Router

  // Filter close btn
  isDropdownOpen = false;
  openDropdown() {
    this.isDropdownOpen = true;
  }

  closeDropdown() {
    this.isDropdownOpen = false;
  }

  ngOnInit(): void {
    this.getPageLinks();
  }

  isPanelOpen = false; // Start with the panel open

  togglePanel() {
    this.isPanelOpen = !this.isPanelOpen; // Toggle the panel state
  }

  getPageLinks(): void {
    this.authService.GetPageLinks().subscribe({
      next: (response: MenuResponse) => {
        console.log('API Response:', response);
        if (response.success) {
          this.menuData = response.data.map((item: any) => ({
            pageId: item.pageId,
            pageNameEng: item.pageNameEng,
            pageNameHin: item.pageNameHin,
            parentNameEng: item.parentNameEng || 'No Parent',
            parentNameHin: item.parentNameHin || 'कोई माता नहीं',
            url: item.url,
            isSubMenu: !!item.isSubMenu
          }));
          this.noRecordsFound = this.menuData.length === 0; // Check if no records were found
        } else {
          this.noRecordsFound = true;
          Swal.fire('Error', 'Failed to fetch page links', 'error');
        }
      },
      error: (error) => {
        console.error('Error fetching page links:', error);
        this.noRecordsFound = true;
        Swal.fire('Error', 'Unable to fetch page links', 'error');
      }
    });
  }
  
  editPageLink(id: number): void {
    this.router.navigate([`/application/mngmenu/${id}`]);
  }

  deletePageLink(pageId: number): void {
    Swal.fire({
        title: 'Are you sure?',
        text: 'Do you want to delete this menu?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Yes, delete it!',
        cancelButtonText: 'No, keep it'
    }).then((result) => {
        if (result.isConfirmed) {
            this.authService.DeletePageLink(pageId).subscribe({
                next: () => {
                    this.menuData = this.menuData.filter(menu => menu.pageId !== pageId);
                    this.noRecordsFound = this.menuData.length === 0; // Update if no records remain
                    Swal.fire('Deleted!', 'Your menu has been deleted.', 'success');
                },
                error: (error) => {
                    console.error('Error deleting menu:', error);
                    Swal.fire('Error', 'Unable to delete menu', 'error');
                }
            });
        }
    });
  }  
}