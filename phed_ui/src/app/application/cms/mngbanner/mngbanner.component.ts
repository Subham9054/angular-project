import { Component, OnInit} from '@angular/core';
import { AuthService } from 'src/app/auth.service';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import Swal from 'sweetalert2';

interface UploadedFile {
  file: File;
  url: string;
}

@Component({
  selector: 'app-mngbanner',
  templateUrl: './mngbanner.component.html',
  styleUrls: ['./mngbanner.component.scss'],
})
export class MngbannerComponent implements OnInit {  
  bannerId: number | undefined;
  files: UploadedFile[] = [];
  bannerHeadingEnglish: string = '';
  bannerHeadingHindi: string = '';
  bannerContentEnglish: string = '';
  bannerContentHindi: string = '';
  serialNo: number | null = null;
  isPublish: boolean = false;
  isEditing: boolean = false;

  constructor(
    private authService: AuthService,
    private route: ActivatedRoute,
    private router: Router,
    private http: HttpClient
  ) {}

  ngOnInit(): void {
    this.checkForEdit();
  }

  // Check if we are in edit mode by looking for the ID in the route parameters
  checkForEdit(): void {
    this.route.params.subscribe((params) => {
      const id = +params['id']; // Convert the route parameter to a number
      if (id) {
        this.isEditing = true;
        this.loadBanner(id);
      } else {
        this.isEditing = false; // If there's no 'id', not in edit mode
      }
    });
  }  

  // Load banner data for editing
  loadBanner(id: number): void {
    this.authService.GetBannerById(id).subscribe({
      next: (response: any) => {
        if (response?.success && response.data && response.data.length > 0) {
          const data = response.data[0];
  
          // Populate form fields
          this.bannerId = data.bannerId;
          this.bannerHeadingEnglish = data.bannerHeadingEng;
          this.bannerHeadingHindi = data.bannerHeadingHin;
          this.bannerContentEnglish = data.bannerContentEng;
          this.bannerContentHindi = data.bannerContentHin;
          this.serialNo = data.serialNo ?? null;
          this.isPublish = !!data.isPublish;
  
          // Handle banner image
          if (data.bannerImage) {
            const baseURL = 'http://localhost:5097'; // Adjust base URL as per your API
            const relativePath = data.bannerImage.replace(/.*wwwroot\\/, '').replace(/\\/g, '/');
            const imageUrl = `${baseURL}/${relativePath}`; // Construct full image URL
            
            // Push image into the files array with preview
            this.files = [{
              file: new File([], relativePath.split('/').pop() || '', { type: 'image/jpeg' }),
              url: imageUrl
            }];
          }
        } else {
          Swal.fire('Error', response?.Message || 'Banner not found', 'error');
          this.router.navigate(['/application/mngbanner']);
        }
      },
      error: (error) => {
        console.error('Error loading banner:', error);
        const errorMessage = error.error?.Message || 'Unable to load banner details.';
        Swal.fire('Error', errorMessage, 'error');
      },
    });
  }
    
  onSelect(event: any): void {
    for (const file of event.addedFiles) {
      const reader = new FileReader();
      reader.onload = (e: any) => {
        this.files.push({ file, url: e.target.result });
      };
      reader.readAsDataURL(file);
    }
  }
  
  onRemove(file: UploadedFile): void {
    this.files = this.files.filter((f) => f !== file);
  }

  submitForm(): void {
    // Validation for required fields
    if (this.bannerHeadingEnglish.trim() === '') {
      Swal.fire('Validation Error', 'Banner Heading in English is required', 'warning');
      return;
    }
    if (this.bannerHeadingHindi.trim() === '') {
      Swal.fire('Validation Error', 'Banner Heading in Hindi is required', 'warning');
      return;
    }
    if (this.bannerContentEnglish.trim() === '') {
      Swal.fire('Validation Error', 'Banner Content in English is required', 'warning');
      return;
    }
    if (this.bannerContentHindi.trim() === '') {
      Swal.fire('Validation Error', 'Banner Content in Hindi is required', 'warning');
      return;
    }
    if (this.serialNo === null) {
      Swal.fire('Validation Error', 'Serial Number is required', 'warning');
      return;
    }
    if (this.files.length === 0 && !this.isEditing) {
      Swal.fire('Validation Error', 'Banner image is required', 'warning');
      return;
    }
  
    // Prepare FormData
    const formData = new FormData();
    formData.append('bannerHeadingEng', this.bannerHeadingEnglish.trim());
    formData.append('bannerHeadingHin', this.bannerHeadingHindi.trim());
    formData.append('bannerContentEng', this.bannerContentEnglish.trim());
    formData.append('bannerContentHin', this.bannerContentHindi.trim());
    formData.append('serialNo', this.serialNo.toString());
    formData.append('isPublish', this.isPublish ? 'true' : 'false');
  
    if (this.bannerId) {
      formData.append('bannerId', this.bannerId.toString());
    }
  
    // Append file (use .file from UploadedFile object)
    if (this.files.length > 0) {
      formData.append('bannerImage', this.files[0].file, this.files[0].file.name);
    }
 
    // Save or update based on editing mode
    this.authService.CreateOrUpdateBanner(formData, this.bannerId).subscribe({
      next: (response: any) => {
        const message = this.isEditing ? 'Banner updated successfully!' : 'Banner created successfully!';
        const errorMessage = this.isEditing ? 'Failed to update banner' : 'Failed to create banner';
        if (response.success) {
          Swal.fire('Success', message, 'success');
          this.router.navigate(['/application/mngbanner/view']);
        } else {
          console.error('API Response:', response);
          Swal.fire('Error', errorMessage, 'error');
        }
        this.resetForm();
      },
      error: (error) => {
        console.error('Error:', error);
        Swal.fire('Error', `An error occurred while ${this.isEditing ? 'updating' : 'creating'} the banner.`, 'error');
      },
    });
  }
  
  resetForm(): void {
    this.files = [];
    this.bannerHeadingEnglish = '';
    this.bannerHeadingHindi = '';
    this.bannerContentEnglish = '';
    this.bannerContentHindi = '';
    this.serialNo = null;
    this.isPublish = false;
    this.isEditing = false;
  }
}