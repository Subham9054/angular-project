import { Component, OnInit } from '@angular/core';
import * as $ from 'jquery';
import 'eonasdan-bootstrap-datetimepicker'; // Include datetimepicker plugin
import * as moment from 'moment';
import { DatePipe } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from 'src/app/auth.service';
import Swal from 'sweetalert2';

// declare let $: any;
@Component({
  selector: 'app-whatsnew',
  templateUrl: './whatsnew.component.html',
  styleUrls: ['./whatsnew.component.scss']
})
export class WhatsnewComponent implements OnInit {
  whatisNewModel: any = {
    whatIsNewId: null,
    titleEnglish: '',
    titleHindi: '',
    descriptionEnglish: '',
    descriptionHindi: '',
    document: '',
    filePath: '',
    fileName: '',
    isPublish: false,
    publishDate: '',
    createdBy: null,
    isEditing: false,
  };

  // file upload
  fileName: string = '';

  // handleFileInput(event: any) {
  //   const file = event.target.files[0];
  //   if (file) {
  //     this.fileName = file.name;
  //   }
  // }


  // handleFileInput(event: any) {
  //   const file = event.target.files[0];
  //   if (file) {
  //     // Validate file size (10 MB limit)
  //     if (file.size > 10 * 1024 * 1024) { 
  //       Swal.fire('Validation Error', 'File size should not exceed 10 MB.', 'error');
  //       return;
  //     }
  
  //     // Validate file extension
  //     const allowedExtensions = ['pdf', 'jpeg', 'jpg', 'png'];
  //     const fileExtension = file.name.split('.').pop()?.toLowerCase();
  //     if (!allowedExtensions.includes(fileExtension || '')) {
  //       Swal.fire('Validation Error', 'Only PDF, JPEG, JPG, or PNG files are allowed.', 'error');
  //       return;
  //     }
  
  //     // If all validations pass, assign file name
  //     this.fileName = file.name;
  //   }
  // }

  handleFileInput(event: any): void {
    const file = event.target.files[0];
    if (file) {
      this.fileName = file.name;
      if (!this.validateFile(this.fileName)) {
        return;
      }
      const reader = new FileReader();
      reader.onload = (e: any) => {
        this.whatisNewModel.filePath = e.target.result;
      };
      reader.readAsDataURL(file);
    }
  }

  validateFile(fileName: string): boolean {
    const allowedExtensions = ['pdf', 'jpeg', 'jpg', 'png'];
    const fileExtension = fileName.split('.').pop()?.toLowerCase();
    if (!allowedExtensions.includes(fileExtension || '')) {
      Swal.fire('Validation Error', 'Only PDF, JPEG, JPG, or PNG files are allowed.', 'error');
      return false;
    }
    return true;
  }

  private isDatePickerInitialized = false;

  constructor(private authService: AuthService, private router: Router, private route: ActivatedRoute, private datePipe: DatePipe) {}

  ngOnInit(): void {
    this.checkForEdit();
    this.whatisNewModel.publishDate = moment().format('DD-MMM-YYYY');; // Set today's date in the model
  } 

  ngAfterViewChecked(): void {
    if (this.whatisNewModel.isPublish && !this.isDatePickerInitialized) {
      this.initializeDatepicker();
      this.isDatePickerInitialized = true;
    } else if (!this.whatisNewModel.isPublish && this.isDatePickerInitialized) {
      this.isDatePickerInitialized = false;
    }
  }

  private initializeDatepicker(): void {
    $('.datepicker').datetimepicker({
      format: 'DD-MMM-YYYY',
      daysOfWeekDisabled: [0, 6], // Disable weekends
      defaultDate: moment(), // Set default date to today
    }).on('dp.change', (e: any) => {
      this.whatisNewModel.publishDate = e.date.format('DD-MMM-YYYY'); // Update the model on date change
    });
  }

  // Check if we are in edit mode by looking for the ID in the route parameters
  checkForEdit(): void {
    this.route.params.subscribe((params) => {
      const id = +params['id']; // Convert the route parameter to a number
      if (id) {
        this.whatisNewModel.isEditing = true;
        this.loadWhatIsNewData(id);
      } else {
        this.whatisNewModel.isEditing = false;
      }
    });
  }

  loadWhatIsNewData(whatIsNewId: number): void {
    debugger;
    this.authService.getWhatIsNewById(whatIsNewId).subscribe({
      next: (response: any) => {
        // Check if the response is successful and contains valid data
        if (response?.success && response.data) {
          // Ensure response.data is an object, not an array
          const data = Array.isArray(response.data) ? response.data[0] : response.data;
  
          // Map the fetched data to the eventModel
          this.whatisNewModel = {
            getWhatIsNewById: data?.getWhatIsNewById || null,
            titleEnglish: data?.titleEnglish || '',
            titleHindi: data?.titleHindi || '',
            descriptionEnglish: data?.descriptionEnglish || '',
            descriptionHindi: data?.descriptionHindi || '',
            document: data?.document || '',
            isPublish: data?.isPublish || false,
            //publishDate: data?.publishDate || '',
            publishDate: data?.publishDate ? this.datePipe.transform(data.publishDate, 'dd-MMM-yyyy') : '',
            isEditing: true, // Ensure editing mode is enabled
          };          
  
          // Handle document/photo
          if (data.document) {
            const baseURL = 'http://localhost:5097'; // Adjust base URL as per your API
            const relativePath = data.whatnews.replace(/.*wwwroot\\/, '').replace(/\\/g, '/');
            const imageUrl = `${baseURL}/${relativePath}`; // Construct full image URL
  
            // Push image into the files array with preview
            // this.fileName = [{
            //   file: new File([], relativePath.split('/').pop() || '', { type: 'image/jpeg' }),
            //   url: imageUrl,
            // }];
          }          
        } else {
          console.error('Error fetching what is new details:', response?.message || 'Invalid response structure.');
          Swal.fire('Error', response?.message || 'Failed to load what is new details.', 'error');
        }
      },
      error: (error) => {
        console.error('Error fetching what is new details:', error);
        Swal.fire('Error', 'Failed to load what is new details.', 'error');
      },
    });
  }

  submitForm(): void {
    debugger;
    // Validation: Title (English)
    if (!this.whatisNewModel.titleEnglish || this.whatisNewModel.titleEnglish.trim() === '') {
      Swal.fire('Validation Error', 'Please enter the Title in English.', 'error');
      return;
    }

    // Validation: Title (Hindi)
    if (!this.whatisNewModel.titleHindi || this.whatisNewModel.titleHindi.trim() === '') {
      Swal.fire('Validation Error', 'कृपया हिंदी में शीर्षक दर्ज करें।', 'error');
      return;
    }

    // Validation: Description (English)
    if (!this.whatisNewModel.descriptionEnglish || this.whatisNewModel.descriptionEnglish.trim() === '') {
      Swal.fire('Validation Error', 'Please enter the Description in English.', 'error');
      return;
    }

    if (this.whatisNewModel.descriptionEnglish.length > 500) {
      Swal.fire('Validation Error', 'Description in English cannot exceed 500 characters.', 'error');
      return;
    }

    // Validation: Description (Hindi)
    if (!this.whatisNewModel.descriptionHindi || this.whatisNewModel.descriptionHindi.trim() === '') {
      Swal.fire('Validation Error', 'कृपया हिंदी में विवरण दर्ज करें।', 'error');
      return;
    }

    if (this.whatisNewModel.descriptionHindi.length > 500) {
      Swal.fire('Validation Error', 'हिंदी में विवरण ५०० अक्षरों से अधिक नहीं हो सकता।', 'error');
      return;
    }

    // if (!this.whatisNewModel.descriptionEnglish || this.whatisNewModel.descriptionEnglish.trim() === '' || this.whatisNewModel.descriptionEnglish.length > 500) {
    //   const errorMessage = !this.whatisNewModel.descriptionEnglish || this.whatisNewModel.descriptionEnglish.trim() === '' 
    //     ? 'कृपया हिंदी में विवरण दर्ज करें।' 
    //     : 'हिंदी में विवरण ५०० अक्षरों से अधिक नहीं हो सकता।';
    //   Swal.fire('Validation Error', errorMessage, 'error');
    //   return;
    // }
    
    // Validate File
    if (this.fileName && !this.validateFile(this.fileName)) {
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
    formData.append('titleEnglish', this.whatisNewModel.titleEnglish);
    formData.append('titleHindi', this.whatisNewModel.titleHindi);
    formData.append('descriptionEnglish', this.whatisNewModel.descriptionEnglish);
    formData.append('descriptionHindi', this.whatisNewModel.descriptionHindi);
    formData.append('isPublish', this.whatisNewModel.isPublish ? 'true' : 'false');
    formData.append('publishDate', this.whatisNewModel.publishDate || '');    

    // Append document/photo file
    if (this.fileName) {
      formData.append('document', this.fileName);
    }

    if (this.whatisNewModel.whatIsNewId) {
      formData.append('whatIsNewId', this.whatisNewModel.whatIsNewId.toString());
    }

    // Submit the form using the authService
    this.authService.createOrUpdateWhatIsNew(formData, this.whatisNewModel.isEditing ? this.whatisNewModel.whatIsNewId : undefined).subscribe({
      next: (response: any) => {
        const message = this.whatisNewModel.isEditing
          ? 'What is new updated successfully!'
          : 'What is new created successfully!';
        Swal.fire('Success', message, 'success');
        this.router.navigate(['/application/whatsnew']);
        this.resetForm();
      },
      error: (error) => {
        console.error('Error:', error);
        Swal.fire('Error', `An error occurred while processing the what's new.`, 'error');
      },
    });
  }
  
  resetForm(): void {
    this.whatisNewModel = {
      whatIsNewId: null,
      titleEnglish: '',
      titleHindi: '',
      descriptionEnglish: '',
      descriptionHindi: '',
      isPublish: false,
      publishDate: moment().format('DD-MMM-YYYY'),
      isEditing: false,
    };
    this.fileName = '';
  }
}