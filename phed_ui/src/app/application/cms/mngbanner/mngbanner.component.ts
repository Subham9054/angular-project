import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { AuthService } from 'src/app/auth.service';
import { ActivatedRoute, Router } from '@angular/router';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-mngbanner',
  templateUrl: './mngbanner.component.html',
  styleUrls: ['./mngbanner.component.scss']
})
export class MngbannerComponent implements OnInit {
  @ViewChild('fileInput') fileInput!: ElementRef;

  files: File[] = [];
  bannerId: number | undefined;
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
    private router: Router
  ) {}

  ngOnInit(): void {
    this.checkForEdit();
  }

  // Check if we are in edit mode by looking for the ID in the route parameters
  checkForEdit(): void {
    this.route.params.subscribe((params) => {
      const id = +params['id'];
      if (id) {
        this.isEditing = true;
        this.loadBanner(id);
      } else {
        this.isEditing = false;
      }
    });
  }

  // Load banner data for editing
  loadBanner(id: number): void {
    this.authService.GetBannerById(id).subscribe({
      next: (response: any) => {
        if (response) {
          this.bannerId = response.bannerId;
          this.bannerHeadingEnglish = response.bannerHeadingEnglish;
          this.bannerHeadingHindi = response.bannerHeadingHindi;
          this.bannerContentEnglish = response.bannerContentEnglish;
          this.bannerContentHindi = response.bannerContentHindi;
          this.serialNo = response.serialNo;
          this.isPublish = response.isPublish;
        } else {
          Swal.fire('Error', 'Banner not found', 'error');
          this.router.navigate(['/application/mngbanner']); // Redirect if banner not found
        }
      },
      error: (error) => {
        Swal.fire('Error', 'Unable to load banner details.', 'error');
      }
    });
  }

  onSelect(event: any): void {
    this.files.push(...event.addedFiles);
  }

  onRemove(event: any): void {
    this.files.splice(this.files.indexOf(event), 1);
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

    if (this.files.length > 0) {
      formData.append('bannerImage', this.files[0], this.files[0].name);
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
      }
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
    this.fileInput.nativeElement.value = '';
  }
}