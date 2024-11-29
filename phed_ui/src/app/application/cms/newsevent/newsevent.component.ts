import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from 'src/app/auth.service';
import Swal from 'sweetalert2';
import * as moment from 'moment'; 

declare let $: any;
@Component({
  selector: 'app-newsevent',
  templateUrl: './newsevent.component.html',
  styleUrls: ['./newsevent.component.scss']
})
export class NewseventComponent implements OnInit {
  eventModel: any = {
    eventId: null,
    titleEnglish: '',
    titleHindi: '',
    descriptionEnglish: '',
    descriptionHindi: '',
    isPublish: false,
    publishDate: '',
    isEditing: false,
  };
  thumbnailFiles: File[] = [];
  featureImageFiles: File[] = [];
  

  constructor(private authService: AuthService, private router: Router, private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.checkForEdit();

    const today = moment().format('DD-MMM-YYYY'); // Get today's date in the desired format
    this.eventModel.publishDate = today; // Set today's date in the model

    // Initialize datetimepicker with today's date as the default
    $('.publishDate').datetimepicker({
      format: 'DD-MMM-YYYY',
      daysOfWeekDisabled: [0, 6], // Example: Disable weekends
      defaultDate: moment(), // Set default date to today
    }).on('dp.change', (e: any) => {
      this.eventModel.publishDate = e.date.format('DD-MMM-YYYY'); // Update the model on date change
    });

    // $('.datepicker').datetimepicker({
    //   format: 'DD-MMM-YYYY',
    //   daysOfWeekDisabled: [0, 6],
    // });
    // $('.timepicker').datetimepicker({
    //   format: 'LT',
    //   daysOfWeekDisabled: [0, 6],
    // });
    // $('.datetimepicker').datetimepicker({
    //   format: 'DD-MMM-YYYY LT',
    //   daysOfWeekDisabled: [0, 6],
    // });
  } 

	onThumbnailSelect(event:any) {
		console.log(event);
		this.thumbnailFiles.push(...event.addedFiles);
	}

	onThumbnailRemove(event:any) {
		console.log(event);
		this.thumbnailFiles.splice(this.thumbnailFiles.indexOf(event), 1);
	}

  onFeatureImageSelect(event:any) {
		console.log(event);
		this.featureImageFiles.push(...event.addedFiles);
	}

	onFeatureImageRemove(event:any) {
		console.log(event);
		this.featureImageFiles.splice(this.thumbnailFiles.indexOf(event), 1);
	}

  // Check if we are in edit mode by looking for the ID in the route parameters
  checkForEdit(): void {
    this.route.params.subscribe((params) => {
      const id = +params['id']; // Convert the route parameter to a number
      if (id) {
        this.eventModel.isEditing = true;
        this.loadEventData(id);
      } else {
        this.eventModel.isEditing = false;
      }
    });
  }

  loadEventData(eventId: number): void {
    debugger;
    this.authService.getEventById(eventId).subscribe({
      next: (response: any) => {
        // Check if the response is successful and contains valid data
        if (response?.success && response.data) {
          // Ensure response.data is an object, not an array
          const data = Array.isArray(response.data) ? response.data[0] : response.data;
  
          // Map the fetched data to the galleryModel
          this.eventModel = {
            eventId: data?.eventId || null,
            titleEnglish: data?.titleEnglish || '',
            titleHindi: data?.titleHindi || '',
            descriptionEnglish: data?.descriptionEnglish || '',
            descriptionHindi: data?.descriptionHindi || '',
            thumbnail: data?.thumbnail || '',
            featureImage: data?.featureImage || '',
            isPublish: data?.isPublish || false,
            publishDate: data?.publishDate || '',
            isEditing: true, // Ensure editing mode is enabled
          };
  
          // Populate files for thumbnails and images if provided by the backend
          this.thumbnailFiles = data?.thumbnail ? this.mapFiles(data.thumbnail) : [];
          this.featureImageFiles = data?.featureImage ? this.mapFiles(data.featureImage) : [];
        } else {
          console.error('Error fetching event details:', response?.message || 'Invalid response structure.');
          Swal.fire('Error', response?.message || 'Failed to load event details.', 'error');
        }
      },
      error: (error) => {
        console.error('Error fetching event details:', error);
        Swal.fire('Error', 'Failed to load event details.', 'error');
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
    debugger;
    // Validation: Title (English)
    if (!this.eventModel.titleEnglish || this.eventModel.titleEnglish.trim() === '') {
      Swal.fire('Validation Error', 'Please enter the Title in English.', 'error');
      return;
    }

    // Validation: Title (Hindi)
    if (!this.eventModel.titleHindi || this.eventModel.titleHindi.trim() === '') {
      Swal.fire('Validation Error', 'कृपया हिंदी में शीर्षक दर्ज करें।', 'error');
      return;
    }

    // Validation: Description (English)
    if (!this.eventModel.descriptionEnglish || this.eventModel.descriptionEnglish.trim() === '') {
      Swal.fire('Validation Error', 'Please enter the Description in English.', 'error');
      return;
    }

    // Validation: Description (Hindi)
    if (!this.eventModel.descriptionHindi || this.eventModel.descriptionHindi.trim() === '') {
      Swal.fire('Validation Error', 'कृपया हिंदी में विवरण दर्ज करें।', 'error');
      return;
    }

    // Validation: Thumbnail Image
    if (!this.thumbnailFiles.length && !this.eventModel.isEditing) {
      Swal.fire('Validation Error', 'Please upload a thumbnail image.', 'error');
      return;
    }

    // Validation: Feature Image
    if (!this.featureImageFiles.length && !this.eventModel.isEditing) {
      Swal.fire('Validation Error', 'Please upload a feature image.', 'error');
      return;
    }

    // Validation: Publish Date if Is Publish is checked
    if (!this.eventModel.isPublish && !this.eventModel.isEditing) {
      Swal.fire('Validation Error', 'Please select the publish', 'error');
      return;
    }

    if (this.eventModel.isPublish && !this.eventModel.publishDate) {
      Swal.fire('Validation Error', 'Please select a publish date.', 'error');
      return;
    }

    // All validations passed; proceed with form submission
    const formData = new FormData();
    formData.append('titleEnglish', this.eventModel.titleEnglish);
    formData.append('titleHindi', this.eventModel.titleHindi);
    formData.append('descriptionEnglish', this.eventModel.descriptionEnglish);
    formData.append('descriptionHindi', this.eventModel.descriptionHindi);
    formData.append('isPublish', this.eventModel.isPublish ? 'true' : 'false');
    formData.append('publishDate', this.eventModel.publishDate || '');

    // Append each thumbnail file individually if available
    if (this.thumbnailFiles.length > 0) {
      this.thumbnailFiles.forEach((file) => {
        formData.append('thumbnail', file, file.name);
      });
    }

    // Append each feature image file individually if available
    if (this.featureImageFiles.length > 0) {
      this.featureImageFiles.forEach((file) => {
        formData.append('featureImage', file, file.name);
      });
    }

    // Submit the form using the authService
    this.authService.createOrUpdateEvent(formData, this.eventModel.isEditing ? this.eventModel.eventId : undefined).subscribe({
      next: (response: any) => {
        const message = this.eventModel.isEditing
          ? 'Event updated successfully!'
          : 'Event created successfully!';
        Swal.fire('Success', message, 'success');
        this.router.navigate(['/application/newsevent']);
        this.resetForm();
      },
      error: (error) => {
        console.error('Error:', error);
        Swal.fire('Error', `An error occurred while processing the event.`, 'error');
      },
    });
  }

  resetForm(): void {
    this.eventModel = {
      eventId: null,
      titleEnglish: '',
      titleHindi: '',
      descriptionEnglish: '',
      descriptionHindi: '',
      isPublish: false,
      publishDate: '',
      isEditing: false,
    };
    this.thumbnailFiles = [];
    this.featureImageFiles = [];
  }
}
