import { Component, AfterViewChecked, OnInit } from '@angular/core';
import * as $ from 'jquery';
import 'eonasdan-bootstrap-datetimepicker'; // Include datetimepicker plugin
import * as moment from 'moment';
import { DatePipe } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from 'src/app/auth.service';
import ClassicEditor from '@ckeditor/ckeditor5-build-classic';
import Swal from 'sweetalert2';

interface UploadedFeatureImageFile {
  file: File;
  url: string;
}

@Component({
  selector: 'app-pagecontent',
  templateUrl: './pagecontent.component.html',
  styleUrls: ['./pagecontent.component.scss']
})
export class PagecontentComponent  implements OnInit, AfterViewChecked {
  //CK Editor
  editor = ClassicEditor;
  data: any = `<p class="text-grey">Enter here...</p>`;

  // Content Model for Form
  contentModel: any = {
    contentId: null,
    pageTitleEnglish: '',
    pageTitleHindi: '',
    pageAlias: '',
    readMore: '',
    linkType: 'Internal',
    windowType: 'New Window',
    snippetEnglish: '',
    snippetHindi: '',
    contentEnglish: '',
    contentHindi: '',
    featureImage: '',
    metaTitle: '',
    metaKeyword: '',
    metaDescription: '',
    isPublish: false,
    publishDate: '',
    isEditing: false,
  };  
  
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
    this.contentModel.publishDate = moment().format('DD-MMM-YYYY'); // Set today's date in the model    
  } 

  ngAfterViewChecked(): void {
    if (this.contentModel.isPublish && !this.isDatePickerInitialized) {
      this.initializeDatepicker();
      this.isDatePickerInitialized = true;
    } else if (!this.contentModel.isPublish && this.isDatePickerInitialized) {
      this.isDatePickerInitialized = false;
    }
  }

  private initializeDatepicker(): void {
    $('.datepicker').datetimepicker({
      format: 'DD-MMM-YYYY',
      daysOfWeekDisabled: [0, 6], // Disable weekends
      defaultDate: moment(), // Set default date to today
    }).on('dp.change', (e: any) => {
      this.contentModel.publishDate = e.date.format('DD-MMM-YYYY'); // Update the model on date change
    });
  }
  
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
        this.contentModel.isEditing = true;
        this.loadPageContentData(id);
      } else {
        this.contentModel.isEditing = false;
      }
    });
  }

  loadPageContentData(contentId: number): void {
    debugger;
    this.authService.getPageContentById(contentId).subscribe({
      next: (response: any) => {
        // Check if the response is successful and contains valid data
        if (response?.success && response.data) {
          // Ensure response.data is an object, not an array
          const data = Array.isArray(response.data) ? response.data[0] : response.data;
  
          // Map the fetched data to the eventModel
          this.contentModel = {
            contentId: data?.contentId || null,
            pageTitleEnglish: data?.pageTitleEnglish || '',
            pageTitleHindi: data?.pageTitleHindi || '',
            pageAlias: data?.pageAlias || '',
            readMore: data?.readMore || '',
            linkType: data?.linkType || '',
            windowType: data?.windowType || '',
            snippetEnglish: data?.snippetEnglish || '',
            snippetHindi: data?.snippetHindi || '',
            contentEnglish: data?.contentEnglish || '',
            contentHindi: data?.contentHindi || '',
            featureImage: data?.featureImage || '',
            metaTitle: data?.metaTitle || '',
            metaKeyword: data?.metaKeyword || '',
            metaDescription: data?.metaDescription || '',
            isPublish: data?.isPublish || false,
            //publishDate: data?.publishDate || '',
            publishDate: data?.publishDate ? this.datePipe.transform(data.publishDate, 'dd-MMM-yyyy') : '',
            isEditing: true, // Ensure editing mode is enabled
          };

          // this.contentModel = {
          //   ...this.contentModel,
          //   ...data,
          //   publishDate: data.publishDate ? this.datePipe.transform(data.publishDate, 'dd-MMM-yyyy') : '',
          //   isEditing: true,
          // };

          // Handle feature image
          if (data.featureImage) {
            const baseURL = 'http://localhost:5097'; // Adjust base URL as per your API
            // const baseURL = 'http://localhost:5234'; // Adjust base URL as per your API
            const relativePath = data.featureImage.replace(/.*wwwroot\\/, '').replace(/\\/g, '/');
            const imageUrl = `${baseURL}/${relativePath}`; // Construct full image URL
  
            // Push image into the files array with preview
            this.featureImageFiles = [{
              file: new File([], relativePath.split('/').pop() || '', { type: 'image/jpeg' }),
              url: imageUrl,
            }];
          }
        } else {
          console.error('Error fetching page content details:', response?.message || 'Invalid response structure.');
          Swal.fire('Error', response?.message || 'Failed to load page content details.', 'error');
        }
      },
      error: (error) => {
        console.error('Error fetching page content details:', error);
        Swal.fire('Error', 'Failed to load page content details.', 'error');
      },
    });
  }

  submitForm(): void {
    debugger;
    // Validation: Page Title (English)
    if (!this.contentModel.pageTitleEnglish || this.contentModel.pageTitleEnglish.trim() === '') {
      Swal.fire('Validation Error', 'Please enter the Page Title in English.', 'error');
      return;
    }

    // Validation: Page Title (Hindi)
    if (!this.contentModel.pageTitleHindi || this.contentModel.pageTitleHindi.trim() === '') {
      Swal.fire('Validation Error', 'कृपया हिंदी में पेज शीर्षक दर्ज करें।', 'error');
      return;
    }

    // Validation: Page Alias
    if (!this.contentModel.pageAlias || this.contentModel.pageAlias.trim() === '') {
      Swal.fire('Validation Error', 'Please enter the Page Alias.', 'error');
      return;
    }

    // Validation: Read More
    if (!this.contentModel.readMore || this.contentModel.readMore.trim() === '') {
      Swal.fire('Validation Error', 'Please enter the Read More text.', 'error');
      return;
    }

    // Validation: Link Type
    const linkType = this.contentModel.linkType || 'Internal'; // Default to 'Internal'
    if (linkType !== 'Internal' && linkType !== 'External') {
      Swal.fire('Validation Error', 'Please select a valid Link Type.', 'error');
      return;
    }

    // Validation: Open in Window
    const windowType = this.contentModel.openWindow || 'New Window'; // Default to 'New Window'
    if (windowType !== 'Same Window' && windowType !== 'New Window') {
      Swal.fire('Validation Error', 'Please select where to open the link (Same Window or New Window).', 'error');
      return;
    }

    // Validation: Snippet (English)
    if (!this.contentModel.snippetEnglish || this.contentModel.snippetEnglish.trim() === '') {
      Swal.fire('Validation Error', 'Please enter the Snippet in English.', 'error');
      return;
    }
    if (this.contentModel.snippetEnglish.length > 500) {
      Swal.fire('Validation Error', 'Snippet (In English) must not exceed 500 characters.', 'error');
      return;
    }

    // // Validation: Snippet (Hindi)
    // if (!this.contentModel.snippetHindi || this.contentModel.snippetHindi.trim() === '') {
    //   Swal.fire('Validation Error', 'कृपया हिंदी में स्निपेट दर्ज करें।', 'error');
    //   return;
    // }
    // if (this.contentModel.snippetHindi.length > 500) {
    //   Swal.fire('Validation Error', 'स्निपेट (हिन्दी में) ५०० अक्षरों से अधिक नहीं होना चाहिए।', 'error');
    //   return;
    // }

    // Validation: Snippet (Hindi)
    if (!this.contentModel.snippetHindi || this.contentModel.snippetHindi.trim() === '' || this.contentModel.snippetHindi.length > 500) {
      Swal.fire(
        'Validation Error',
        !this.contentModel.snippetHindi.trim()
          ? 'कृपया हिंदी में स्निपेट दर्ज करें।'
          : 'स्निपेट (हिन्दी में) ५०० अक्षरों से अधिक नहीं होना चाहिए।',
        'error'
      );
      return;
    }    

    // Validation: Page Content (English)
    if (!this.contentModel.contentEnglish || this.contentModel.contentEnglish.trim() === '') {
      Swal.fire('Validation Error', 'Please enter the Page Content in English.', 'error');
      return;
    }
    if (this.contentModel.contentEnglish.length > 700) {
      Swal.fire('Validation Error', 'Page Content (In English) must not exceed 700 characters.', 'error');
      return;
    }

    // Validation: Page Content (Hindi)
    if (!this.contentModel.contentHindi || this.contentModel.contentHindi.trim() === '') {
      Swal.fire('Validation Error', 'कृपया पृष्ठ सामग्री हिंदी में दर्ज करें।', 'error');
      return;
    }
    if (this.contentModel.contentHindi.length > 700) {
      Swal.fire('Validation Error', 'पृष्ठ सामग्री (हिंदी में) ७०० अक्षरों से अधिक नहीं हो सकती।', 'error');
      return;
    }

    // Validation: Feature Image
    if (!this.featureImageFiles.length && !this.contentModel.isEditing) {
      Swal.fire('Validation Error', 'Please upload a feature image.', 'error');
      return;
    }

    // Validation: Meta Tile
    if (!this.contentModel.metaTitle || this.contentModel.metaTitle.trim() === '') {
      Swal.fire('Validation Error', 'Please enter the Meta Title.', 'error');
      return;
    }

    // Validation: Meta Keyword
    if (!this.contentModel.metaKeyword || this.contentModel.metaKeyword.trim() === '') {
      Swal.fire('Validation Error', 'Please enter the Meta Keyword.', 'error');
      return;
    }

    // Validation: Meta Description
    if (!this.contentModel.metaDescription || this.contentModel.metaDescription.trim() === '') {
      Swal.fire('Validation Error', 'Please enter the Meta Description.', 'error');
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
    formData.append('pageTitleEnglish', this.contentModel.pageTitleEnglish);
    formData.append('pageTitleHindi', this.contentModel.pageTitleHindi);
    formData.append('descriptionEnglish', this.contentModel.descriptionEnglish);
    formData.append('pageAlias', this.contentModel.pageAlias);
    formData.append('readMore', this.contentModel.readMore);
    formData.append('linkType', this.contentModel.linkType);
    formData.append('windowType', this.contentModel.windowType);
    formData.append('snippetEnglish', this.contentModel.snippetEnglish);
    formData.append('snippetHindi', this.contentModel.snippetHindi);
    formData.append('contentEnglish', this.contentModel.contentEnglish);
    formData.append('contentHindi', this.contentModel.contentHindi);
    formData.append('metaTitle', this.contentModel.metaTitle);
    formData.append('metaKeyword', this.contentModel.metaKeyword);
    formData.append('metaDescription', this.contentModel.metaDescription);
    formData.append('isPublish', this.contentModel.isPublish ? 'true' : 'false');
    formData.append('publishDate', this.contentModel.publishDate || '');

    // Append feature image file (use .file from UploadedFile object)
    if (this.featureImageFiles.length > 0) {
      formData.append('featureImage', this.featureImageFiles[0].file, this.featureImageFiles[0].file.name);
    }

    if (this.contentModel.contentId) {
      formData.append('contentId', this.contentModel.contentId.toString());
    }

    // Submit the form using the authService
    this.authService.createOrUpdatePageContent(formData, this.contentModel.isEditing ? this.contentModel.contentId : undefined).subscribe({
      next: (response: any) => {
        const message = this.contentModel.isEditing
          ? 'Page Content Updated Successfully!'
          : 'Page Content Created Successfully!';
        Swal.fire('Success', message, 'success');
        this.router.navigate(['/application/pagecontent']);
        this.resetForm();
      },
      error: (error) => {
        console.error('Error:', error);
        Swal.fire('Error', `An error occurred while processing the page content.`, 'error');
      },
    });
  }

  resetForm(): void {
    this.contentModel = {
      contentId: null,
      pageTitleEnglish: '',
      pageTitleHindi: '',
      pageAlias: '',
      readMore: '',
      linkType: '',
      windowType: '',
      snippetEnglish: '',
      snippetHindi: '',
      contentEnglish: '',
      contentHindi: '',
      featureImage: '',
      metaTitle: '',
      metaKeyword: '',
      metaDescription: '',
      isPublish: false,
      publishDate: '',
      isEditing: false,
    };
    this.featureImageFiles = [];
  }
}