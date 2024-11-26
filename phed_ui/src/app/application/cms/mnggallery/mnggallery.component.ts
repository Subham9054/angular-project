import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/auth.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-mnggallery',
  templateUrl: './mnggallery.component.html',
  styleUrls: ['./mnggallery.component.scss']
})
export class MnggalleryComponent implements OnInit {
  galleryModel: any = {
    galleryCategory: '',
    galleryType: '',
    videoUrl: '',
    captionEnglish: '',
    captionHindi: '',
    serialNo: null,
    isPublish: false,
    isEditing: false,
  };

  selectedType: string = "";
  thumbnailFiles: File[] = [];
  imageFiles: File[] = [];

  constructor(private authService: AuthService, private router: Router) {}

  ngOnInit(): void {}

  onGalleryTypeChange() {
    this.selectedType = this.galleryModel.galleryType;
  }

  onThumbnailSelect(event:any) {
		console.log(event);
		this.thumbnailFiles.push(...event.addedFiles);
	}

	onThumbnailRemove(event:any) {
		console.log(event);
		this.thumbnailFiles.splice(this.thumbnailFiles.indexOf(event), 1);
	}

  onImageSelect(event:any) {
		console.log(event);
		this.imageFiles.push(...event.addedFiles);
	}

  onImageRemove(event:any) {
		console.log(event);
		this.imageFiles.splice(this.imageFiles.indexOf(event), 1);
	}
  

  submitForm(): void {
    // Validate Category
    if (!this.galleryModel.galleryCategory) {
      Swal.fire('Validation Error', 'Please select a category.', 'error');
      return;
    }

    // Validate Type
    if (!this.galleryModel.galleryType) {
      Swal.fire({
        icon: 'error',
        title: 'Validation Error',
        text: 'Please select a type.',
      });
      return;
    }
  
    // Validate Thumbnail
    if (!this.thumbnailFiles?.length && !this.galleryModel.isEditing) {
      Swal.fire('Validation Error', 'Please upload a thumbnail image.', 'error');
      return;
    }

    // Validate Photo for Type 'photo'  
    if (this.galleryModel.galleryType === 'photo' && !this.imageFiles?.length && !this.galleryModel.isEditing) {
      Swal.fire('Validation Error', 'Please upload a photo.', 'error');
      return;
    }

    // Validate Video URL for Type 'video'
    if (this.galleryModel.galleryType === 'video' && !this.galleryModel.videoUrl) {
      Swal.fire('Validation Error', 'Please enter a video URL.', 'error');
      return;
    }
  
    // Validate Caption (English)
    if (!this.galleryModel.captionEnglish) {
      Swal.fire('Validation Error', 'Please enter the caption in English.', 'error');
      return;
    }

    // Validate Caption (Hindi)
    if (!this.galleryModel.captionHindi) {
      Swal.fire('Validation Error', 'Please enter the caption in Hindi.', 'error');
      return;
    }

    // Validate Serial Number
    if (!this.galleryModel.serialNo) {
      Swal.fire('Validation Error', 'Please enter the serial number.', 'error');
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

    // For thumbnail files
    if (this.thumbnailFiles && this.thumbnailFiles.length > 0) {
      this.thumbnailFiles.forEach((file, index) => {
        formData.append('thumbnail', file, file.name); // Append each file individually
      });
    }

    // For image files
    if (this.imageFiles && this.imageFiles.length > 0) {
      this.imageFiles.forEach((file, index) => {
        formData.append('image', file, file.name); // Append each file individually
      });
    }
  
    // Save or update based on editing mode
    this.authService.createOrUpdateGallery(formData, this.galleryModel.isEditing ? this.galleryModel.galleryId : undefined).subscribe({
      next: (response: any) => {
        const message = this.galleryModel.isEditing ? 'Gallery updated successfully!' : 'Gallery created successfully!';
        const errorMessage = this.galleryModel.isEditing ? 'Failed to update gallery.' : 'Failed to create gallery.';
        if (response.success) {
          Swal.fire('Success', message, 'success');
          this.router.navigate(['/application/mnggallery']);
        } else {
          console.error('API Response:', response);
          Swal.fire('Error', errorMessage, 'error');
        }
        this.resetForm();
      },
      error: (error) => {
        console.error('Error:', error);
        Swal.fire(
          'Error',
          `An error occurred while ${this.galleryModel.isEditing ? 'updating' : 'creating'} the gallery.`,
          'error'
        );
      },
    });
  }

  resetForm(): void {
    this.galleryModel = {
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
