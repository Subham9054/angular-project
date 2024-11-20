import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { AuthService } from 'src/app/auth.service';
import { ActivatedRoute } from '@angular/router';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-mngmenu',
  templateUrl: './mngmenu.component.html',
  styleUrls: ['./mngmenu.component.scss']
})
export class MngmenuComponent implements OnInit {
  pageId: number | undefined;
  pageNameEng: string = '';
  pageNameHin: string = '';
  isSubMenu: boolean = false;
  parentMenuNames: { name: string, id: number, nameHin: string }[] = [];
  selectedParentMenu: number = 0;
  selectedParentMenuName: string = '';
  position: number = 0;
  isExternalValue: boolean = false;
  url: string = '';
  iconFile: File | null = null; // Holds the selected file
  imagePreview: string | ArrayBuffer | null = null; // Variable for image preview
  existingIconUrl: string | null = null; // Holds the existing icon URL
  @ViewChild('fileInput') fileInput!: ElementRef; // Reference to the file input element

  isEditing: boolean = false;

  constructor(
    private authService: AuthService, 
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.loadParentMenus();
    this.checkForEdit(); 
  }

  loadParentMenus(): void {
    this.authService.GetParentMenus().subscribe({
      next: (response: any) => {
        if (response.success && response.data) {
          this.parentMenuNames = response.data.map((menu: any) => ({
            name: menu.parentMenu,
            id: menu.pageId
          }));
          console.log('Parent Menus Loaded:', this.parentMenuNames);
        }
      },
      error: (error) => {
        console.error('Error fetching parent menus:', error);
      }
    });
  }

  checkForEdit(): void {
    this.route.params.subscribe(params => {
      const id = params['id']; // Assuming `id` is passed in the route as a parameter
      if (id) {
        const parsedId = parseInt(id, 10);
        if (!isNaN(parsedId)) {
          this.isEditing = true;
          this.pageId = parsedId;
          this.authService.GetPageLinkById(parsedId).subscribe({
            next: (response: any) => {
              if (response.success && response.data && response.data.length > 0) {
                this.populateForm(response.data[0]);
              } else {
                Swal.fire('Error', 'Page not found or invalid data', 'error');
              }
            },
            error: (error) => {
              console.error('Error fetching menu data for editing:', error);
              Swal.fire('Error', 'Unable to fetch menu data for editing', 'error');
            }
          });
        } else {
          console.warn('Invalid pageId provided');
        }
      }
    });
  }    

  populateForm(data: any): void {
    this.pageNameEng = data.pageNameEng;
    this.pageNameHin = data.pageNameHin;
    this.isSubMenu = data.parentId > 0;
    this.selectedParentMenu = data.parentId;
    this.selectedParentMenuName = data.parentMenu;
    this.position = data.position;
    this.isExternalValue = data.isExternal === 1;
    this.url = data.url;

    if (data.icon) {
      const relativePath = data.icon.replace(/.*wwwroot\\/, '').replace(/\\/g, '/');
      const baseURL = 'http://localhost:5097/';
      this.imagePreview = `${baseURL}${relativePath}`;
      this.existingIconUrl = relativePath; // Save existing icon URL for reference
    } else {
      this.imagePreview = null;
      this.existingIconUrl = null;
    }    
  }

  toggleSubMenu(value: boolean): void {
    this.isSubMenu = value;
  }

  onParentMenuChange(event: Event): void {
    const selectElement = event.target as HTMLSelectElement;
    this.selectedParentMenu = parseInt(selectElement.value, 10);
    const selectedMenu = this.parentMenuNames.find(menu => menu.id === this.selectedParentMenu);
    this.selectedParentMenuName = selectedMenu ? selectedMenu.name : '';
  }

  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files[0]) {
      const file = input.files[0];
      if (file.type === 'image/jpeg' || file.type === 'image/png') {
        this.iconFile = file;
        
        const reader = new FileReader();
        reader.onload = () => {
          this.imagePreview = reader.result;
        };
        reader.readAsDataURL(file);
      } else {
        Swal.fire('Error', 'Only JPEG and PNG files are allowed', 'error');
      }
    }
  }

  submitForm(): void {
    debugger;
    // Validate required fields
    if (!this.pageNameEng || !this.pageNameHin) {
      Swal.fire('Error', 'Please fill in all mandatory fields', 'error');
      return;
    }
    
    // Validate that a parent menu is selected if it's a sub-menu
    if (this.isSubMenu && this.selectedParentMenu <= 0) {
      Swal.fire('Error', 'Please select a parent menu for the sub-menu', 'error');
      return;
    }
  
    // Prepare the FormData to send
    const formData = new FormData();

    // Check if the data is valid before appending
    if (this.pageId) formData.append('pageId', this.pageId.toString());
    formData.append('pageNameEng', this.pageNameEng || '');
    formData.append('pageNameHin', this.pageNameHin || '');
    formData.append('parentMenu', this.selectedParentMenuName || '');
    formData.append('isSubMenu', this.isSubMenu ? 'true' : 'false');
    formData.append('parentId', this.isSubMenu ? this.selectedParentMenu.toString() : '0');
    formData.append('parentNameEng', this.isSubMenu ? this.selectedParentMenuName : '');
    formData.append('parentNameHin', this.isSubMenu ? this.selectedParentMenuName : '');
    formData.append('position', this.isSubMenu ? this.position.toString() : '0');
    formData.append('isExternal', this.isExternalValue ? 'true' : 'false');
    formData.append('url', this.url || '');
  
    if (this.iconFile) {
      // Append the new icon file if one was selected
      formData.append('iconFile', this.iconFile, this.iconFile.name);
    } else if (this.existingIconUrl) {
      // If no new file selected, retain the existing icon URL
      formData.append('existingIconUrl', this.existingIconUrl);
    }
  
    // Call the service method to send the form data
    this.authService.CreateOrUpdatePageLink(formData, this.pageId).subscribe({
      next: (response: any) => {
        const message = this.isEditing ? 'Page updated successfully' : 'Page created successfully';
        const errorMessage = this.isEditing ? 'Failed to update page' : 'Failed to create page';
        if (response.success) {
          Swal.fire('Success', message, 'success');
        } else {
          console.error('API Response:', response);
          Swal.fire('Error', errorMessage, 'error');
        }
        this.resetForm();
      },
      error: (error) => {
        console.error('Error creating/updating page:', error);
        Swal.fire('Error', `An error occurred while ${this.isEditing ? 'updating' : 'creating'} the page: ${error.message || error}`, 'error');
    }});
  }

  resetForm(): void {
    this.pageNameEng = '';
    this.pageNameHin = '';
    this.isSubMenu = false;
    this.selectedParentMenu = 0;
    this.selectedParentMenuName = '';
    this.position = 0;
    this.isExternalValue = false;
    this.url = '';
    this.iconFile = null;
    this.imagePreview = null;
    this.existingIconUrl = null;
    this.isEditing = false;
    this.fileInput.nativeElement.value = '';
  }
}