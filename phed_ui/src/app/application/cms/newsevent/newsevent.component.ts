import { Component, AfterViewChecked, OnInit } from '@angular/core';
import * as $ from 'jquery';
import 'eonasdan-bootstrap-datetimepicker'; // Include datetimepicker plugin
import * as moment from 'moment';
import { DatePipe } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from 'src/app/auth.service';
import Swal from 'sweetalert2';

//declare let $: any; // Declare jQuery

interface UploadedThumbnailFile {
  file: File;
  url: string;
}

interface UploadedFeatureImageFile {
  file: File;
  url: string;
}

@Component({
  selector: 'app-newsevent',
  templateUrl: './newsevent.component.html',
  styleUrls: ['./newsevent.component.scss']
})
export class NewseventComponent implements OnInit, AfterViewChecked {
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
  
  thumbnailFiles: UploadedThumbnailFile[] = [];
  featureImageFiles: UploadedFeatureImageFile[] = [];  

  private isDatePickerInitialized = false;

  constructor(
    private authService: AuthService,
    private router: Router,
    private route: ActivatedRoute,
    private datePipe: DatePipe
  ) {}

  ngOnInit(): void {
    this.checkForEdit();

    // Set default date if needed
    const today = moment().format('DD-MMM-YYYY'); // Get today's date in the desired format
    this.eventModel.publishDate = today; // Set today's date in the model

    // $('.datepicker').datetimepicker({
    //   format: 'DD-MMM-YYYY',
    //   daysOfWeekDisabled: [0, 6], // Disable weekends
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

  ngAfterViewChecked(): void {
    if (this.eventModel.isPublish && !this.isDatePickerInitialized) {
      this.initializeDatepicker();
      this.isDatePickerInitialized = true;
    } else if (!this.eventModel.isPublish && this.isDatePickerInitialized) {
      this.isDatePickerInitialized = false;
    }
  }

  private initializeDatepicker(): void {
    $('.datepicker').datetimepicker({
      format: 'DD-MMM-YYYY',
      daysOfWeekDisabled: [0, 6], // Disable weekends
      defaultDate: moment(), // Set default date to today
    }).on('dp.change', (e: any) => {
      this.eventModel.publishDate = e.date.format('DD-MMM-YYYY'); // Update the model on date change
    });
  }

  // onThumbnailSelect(event: any): void {
  //   for (const file of event.addedFiles) {
  //     const reader = new FileReader();
  //     reader.onload = (e: any) => {
  //       this.thumbnailFiles.push({ file, url: e.target.result });
  //     };
  //     reader.readAsDataURL(file);
  //   }
  // }
  onThumbnailSelect(event: any): void {
    for (const file of event.addedFiles) {
      // Validate file size
      if (file.size > 300 * 1024 ) {
        Swal.fire('Error', 'File exceeds maximum size of 300KB', 'error');
        continue; // Skip this file
      }
  
      // Validate file type
      if (!file.type.startsWith('image/')) {
        Swal.fire('Error', 'Only image files are allowed', 'error');
        continue; // Skip this file
      }
  
      // Read and preview valid files
      const reader = new FileReader();
      reader.onload = (e: any) => {
        this.thumbnailFiles.push({ file, url: e.target.result });
      };
      reader.readAsDataURL(file);
    }
  }
  
  
  onThumbnailRemove(file: UploadedThumbnailFile): void {
    this.thumbnailFiles = this.thumbnailFiles.filter((f) => f !== file);
  }

  // onFeatureImageSelect(event: any): void {
  //   for (const file of event.addedFiles) {
  //     const reader = new FileReader();
  //     reader.onload = (e: any) => {
  //       this.featureImageFiles.push({ file, url: e.target.result });
  //     };
  //     reader.readAsDataURL(file);
  //   }
  // }
  onFeatureImageSelect(event: any): void {
    for (const file of event.addedFiles) {
      // Validate file size
      if (file.size > 1 * 1024 * 1024) {
        Swal.fire('Error', 'File exceeds maximum size of 1MB', 'error');
        continue; // Skip this file
      }
  
      // Validate file type
      if (!file.type.startsWith('image/')) {
        Swal.fire('Error', 'Only image files are allowed', 'error');
        continue; // Skip this file
      }
  
      // Read and preview valid files
      const reader = new FileReader();
      reader.onload = (e: any) => {
        this.featureImageFiles.push({ file, url: e.target.result });
      };
      reader.readAsDataURL(file);
    }
  }
  
  onFeatureImageRemove(file: UploadedFeatureImageFile): void {
    this.featureImageFiles = this.featureImageFiles.filter((f) => f !== file);
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
  
          // Map the fetched data to the eventModel
          this.eventModel = {
            eventId: data?.eventId || null,
            titleEnglish: data?.titleEnglish || '',
            titleHindi: data?.titleHindi || '',
            descriptionEnglish: data?.descriptionEnglish || '',
            descriptionHindi: data?.descriptionHindi || '',
            thumbnail: data?.thumbnail || '',
            featureImage: data?.featureImage || '',
            isPublish: data?.isPublish || false,
            //publishDate: data?.publishDate || '',
            publishDate: data?.publishDate ? this.datePipe.transform(data.publishDate, 'dd-MMM-yyyy') : '',
            isEditing: true, // Ensure editing mode is enabled
          };          
  
          // Handle thumbnail image
          if (data.thumbnail) {
            const baseURL = 'http://localhost:5097'; // Adjust base URL as per your API
            const relativePath = data.thumbnail.replace(/.*wwwroot\\/, '').replace(/\\/g, '/');
            const imageUrl = `${baseURL}/${relativePath}`; // Construct full image URL
  
            // Push image into the files array with preview
            this.thumbnailFiles = [{
              file: new File([], relativePath.split('/').pop() || '', { type: 'image/jpeg' }),
              url: imageUrl,
            }];
          }

          // Handle feature image
          if (data.featureImage) {
            const baseURL = 'http://localhost:5097'; // Adjust base URL as per your API
            const relativePath = data.featureImage.replace(/.*wwwroot\\/, '').replace(/\\/g, '/');
            const imageUrl = `${baseURL}/${relativePath}`; // Construct full image URL
  
            // Push image into the files array with preview
            this.featureImageFiles = [{
              file: new File([], relativePath.split('/').pop() || '', { type: 'image/jpeg' }),
              url: imageUrl,
            }];
          }
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

    // // Validation: Publish Date if Is Publish is checked
    // if (!this.eventModel.isPublish && !this.eventModel.isEditing) {
    //   Swal.fire('Validation Error', 'Please select the publish', 'error');
    //   return;
    // }

    // if (this.eventModel.isPublish && !this.eventModel.publishDate) {
    //   Swal.fire('Validation Error', 'Please select a publish date.', 'error');
    //   return;
    // }

    // All validations passed; proceed with form submission
    const formData = new FormData();
    formData.append('titleEnglish', this.eventModel.titleEnglish);
    formData.append('titleHindi', this.eventModel.titleHindi);
    formData.append('descriptionEnglish', this.eventModel.descriptionEnglish);
    formData.append('descriptionHindi', this.eventModel.descriptionHindi);
    formData.append('isPublish', this.eventModel.isPublish ? 'true' : 'false');
    formData.append('publishDate', this.eventModel.publishDate || '');

    // Append thumbnail file (use .file from UploadedFile object)
    if (this.thumbnailFiles.length > 0) {
      formData.append('thumbnail', this.thumbnailFiles[0].file, this.thumbnailFiles[0].file.name);
    }

    // Append feature image file (use .file from UploadedFile object)
    if (this.featureImageFiles.length > 0) {
      formData.append('featureImage', this.featureImageFiles[0].file, this.featureImageFiles[0].file.name);
    }

    if (this.eventModel.eventId) {
      formData.append('eventId', this.eventModel.eventId.toString());
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