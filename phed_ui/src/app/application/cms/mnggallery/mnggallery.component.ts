import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, NavigationEnd } from '@angular/router';
import { Subscription } from 'rxjs';
import { AuthService } from 'src/app/auth.service';

import Swal from 'sweetalert2';

@Component({
  selector: 'app-mnggallery',
  templateUrl: './mnggallery.component.html',
  styleUrls: ['./mnggallery.component.scss'],
})
export class MnggalleryComponent implements OnInit {
  galleryModel: any = {
    galleryId: null,
    galleryCategory: '',
    galleryType: '',
    videoUrl: '',
    captionEnglish: '',
    captionHindi: '',
    serialNo: null,
    isPublish: false,
    isEditing: false,
  };

  selectedType: string = '';
  thumbnailFiles: File[] = [];
  imageFiles: File[] = [];
  routerSubscription!: Subscription;

  constructor(private authService: AuthService, private router: Router, private route: ActivatedRoute) {}

  ngOnInit(): void {
    debugger;
    this.checkForEdit();
  }

  // Check if we are in edit mode by looking for the ID in the route parameters
  checkForEdit(): void {
    this.route.params.subscribe((params) => {
      const id = +params['id']; // Convert the route parameter to a number
      if (id) {
        this.galleryModel.isEditing = true; // Use galleryModel.isEditing
        this.loadGalleryData(id);
      } else {
        this.galleryModel.isEditing = false; // Use galleryModel.isEditing
      }
    });
  }

  onGalleryTypeChange() {
    this.selectedType = this.galleryModel.galleryType;
  }

  onThumbnailSelect(event: any) {
    console.log(event);
    this.thumbnailFiles.push(...event.addedFiles);
  }

  onThumbnailRemove(event: any) {
    console.log(event);
    this.thumbnailFiles.splice(this.thumbnailFiles.indexOf(event), 1);
  }

  onImageSelect(event: any) {
    console.log(event);
    this.imageFiles.push(...event.addedFiles);
  }

  onImageRemove(event: any) {
    console.log(event);
    this.imageFiles.splice(this.imageFiles.indexOf(event), 1);
  }

  loadGalleryData(galleryId: number): void {
    debugger;
    this.authService.getGalleryById(galleryId).subscribe({
      next: (response: any) => {
        // Check if the response is successful and contains valid data
        if (response?.success && response.data) {
          // Ensure response.data is an object, not an array
          const data = Array.isArray(response.data) ? response.data[0] : response.data;
  
          // Map the fetched data to the galleryModel
          this.galleryModel = {
            galleryId: data?.galleryId || null,
            galleryCategory: data?.galleryCategory || '',
            galleryType: data?.galleryType || '',
            videoUrl: data?.videoUrl || '',
            captionEnglish: data?.captionEnglish || '',
            captionHindi: data?.captionHindi || '',
            serialNo: data?.serialNo || null,
            isPublish: data?.isPublish || false,
            isEditing: true, // Ensure editing mode is enabled
          };
  
          // Update selectedType based on gallery type
          this.selectedType = this.galleryModel.galleryType;
  
          // Populate files for thumbnails and images if provided by the backend
          this.thumbnailFiles = data?.thumbnail ? this.mapFiles(data.thumbnail) : [];
          this.imageFiles = data?.image ? this.mapFiles(data.image) : [];
        } else {
          console.error('Error fetching gallery details:', response?.message || 'Invalid response structure.');
          Swal.fire('Error', response?.message || 'Failed to load gallery details.', 'error');
        }
      },
      error: (error) => {
        console.error('Error fetching gallery details:', error);
        Swal.fire('Error', 'Failed to load gallery details.', 'error');
      },
    });
  }
  
  

  private mapFiles(files: any[]): File[] {
    return files.map((fileData: any) => {
      const blob = new Blob([fileData.content], { type: fileData.type });
      return new File([blob], fileData.name, { type: fileData.type });
    });
  }

  submitForm(): void {
    // Validation checks
    if (!this.galleryModel.galleryCategory) {
      Swal.fire('Validation Error', 'Please select a category.', 'error');
      return;
    }

    if (!this.galleryModel.galleryType) {
      Swal.fire('Validation Error', 'Please select a type.', 'error');
      return;
    }

    if (!this.thumbnailFiles.length && !this.galleryModel.isEditing) {
      Swal.fire('Validation Error', 'Please upload a thumbnail image.', 'error');
      return;
    }

    if (this.galleryModel.galleryType === 'photo' && !this.imageFiles.length && !this.galleryModel.isEditing) {
      Swal.fire('Validation Error', 'Please upload a photo.', 'error');
      return;
    }

    if (this.galleryModel.galleryType === 'video' && !this.galleryModel.videoUrl) {
      Swal.fire('Validation Error', 'Please enter a video URL.', 'error');
      return;
    }

    if (!this.galleryModel.captionEnglish) {
      Swal.fire('Validation Error', 'Please enter the caption in English.', 'error');
      return;
    }

    // All validations passed; proceed with form submission
    const formData = new FormData();
    formData.append('galleryCategory', this.galleryModel.galleryCategory);
    formData.append('galleryType', this.galleryModel.galleryType);
    formData.append('videoUrl', this.galleryModel.videoUrl || '');
    formData.append('captionEnglish', this.galleryModel.captionEnglish);
    formData.append('captionHindi', this.galleryModel.captionHindi || '');
    formData.append('serialNo', this.galleryModel.serialNo ? this.galleryModel.serialNo.toString() : '');
    formData.append('isPublish', this.galleryModel.isPublish ? 'true' : 'false');

    // Append each thumbnail file individually if available
    if (this.thumbnailFiles.length > 0) {
      this.thumbnailFiles.forEach((file) => {
        formData.append('thumbnail', file, file.name);
      });
    }

    // Append each image file individually if available
    if (this.imageFiles.length > 0) {
      this.imageFiles.forEach((file) => {
        formData.append('image', file, file.name);
      });
    }

    this.authService.createOrUpdateGallery(formData, this.galleryModel.isEditing ? this.galleryModel.galleryId : undefined).subscribe({
      next: (response: any) => {
        const message = this.galleryModel.isEditing
          ? 'Gallery updated successfully!'
          : 'Gallery created successfully!';
        Swal.fire('Success', message, 'success');
        this.router.navigate(['/application/mnggallery']);
        this.resetForm();
      },
      error: (error) => {
        console.error('Error:', error);
        Swal.fire('Error', `An error occurred while processing the gallery.`, 'error');
      },
    });
  }

  resetForm(): void {
    this.galleryModel = {
      galleryId: null,
      galleryCategory: '',
      galleryType: '',
      videoUrl: '',
      captionEnglish: '',
      captionHindi: '',
      serialNo: null,
      isPublish: false,
      isEditing: false,
    };
    this.thumbnailFiles = [];
    this.imageFiles = [];
    this.selectedType = '';
  }
}
