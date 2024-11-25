import { Component, OnInit } from '@angular/core';
import ClassicEditor from '@ckeditor/ckeditor5-build-classic';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from 'src/app/auth.service';
import { ActivatedRoute } from '@angular/router';
import Swal from 'sweetalert2';

export interface FAQ {
  faqId: number;
  faqEng: string;
  faqHin: string;
  faqAnsEng: string;
  faqAnsHin: string;
  createdBy: number;
}

@Component({
  selector: 'app-mngfaq',
  templateUrl: './mngfaq.component.html',
  styleUrls: ['./mngfaq.component.scss'],
})
export class MngfaqComponent implements OnInit {
  editor = ClassicEditor;
  faqForm: FormGroup;
  isEditMode = false;

  constructor(private fb: FormBuilder, private authService: AuthService, private route: ActivatedRoute) {
    // Initialize form group, including `faqId` as a hidden control
    this.faqForm = this.fb.group({
      faqId: [0], // Default to 0 for new records
      faqEng: ['', [Validators.required, Validators.maxLength(200)]],
      faqHin: ['', [Validators.required, Validators.maxLength(200)]],
      faqAnsEng: ['', [Validators.required, Validators.maxLength(500)]],
      faqAnsHin: ['', [Validators.required, Validators.maxLength(500)]],
    });
  }

  ngOnInit(): void {
    this.checkForEdit(); // Check if in edit mode based on route params
  }

  // Check if the user is editing an existing FAQ
  checkForEdit(): void {
    this.route.params.subscribe((params) => {
      const id = params['id'];
      if (id) {
        const faqId = parseInt(id, 10);
        if (!isNaN(faqId)) {
          this.isEditMode = true;
          this.authService.getFAQById(faqId).subscribe({
            next: (faqResponse) => this.populateForm(faqResponse),
            error: () =>
              Swal.fire('Error', 'Failed to load FAQ for editing.', 'error'),
          });
        }
      }
    });
  }

  // Populate the form with existing FAQ data
  populateForm(faqResponse: any): void {
    const faq = faqResponse?.data?.[0]; // Access the first object in the data array
    if (!faq) {
      Swal.fire('Error', 'No FAQ data found for this ID.', 'error');
      return;
    }

    // Bind data to form controls
    this.faqForm.patchValue({
      faqId: faq.faqId,
      faqEng: faq.faqEng || '',
      faqHin: faq.faqHin || '',
      faqAnsEng: faq.faqAnsEng || '',
      faqAnsHin: faq.faqAnsHin || '',
    });
  }

  // Handle form submission
  onSubmit(): void {
    // Check if the form is invalid
    if (this.faqForm.invalid) {
      // Check individual fields for validation errors
      if (this.faqForm.get('faqEng')?.invalid) {
        Swal.fire(
          'Validation Error',
          'FAQ (English) must be filled correctly. Maximum 200 characters allowed.',
          'warning'
        );
        return;
      }
  
      if (this.faqForm.get('faqHin')?.invalid) {
        Swal.fire(
          'Validation Error',
          'एफएक्यू (हिन्दी) must be filled correctly. Maximum 200 characters allowed.',
          'warning'
        );
        return;
      }
  
      if (this.faqForm.get('faqAnsEng')?.invalid) {
        Swal.fire(
          'Validation Error',
          'FAQ Answer (English) must be filled correctly. Maximum 500 characters allowed.',
          'warning'
        );
        return;
      }
  
      if (this.faqForm.get('faqAnsHin')?.invalid) {
        Swal.fire(
          'Validation Error',
          'एफएक्यू उत्तर (हिन्दी) must be filled correctly. Maximum 500 characters allowed.',
          'warning'
        );
        return;
      }
      return;
    }
  
    // If all validations pass, proceed to save or update the FAQ
    const faqData: FAQ = this.faqForm.value;
  
    // Create or update FAQ based on mode
    this.authService.createOrUpdateFAQ(faqData).subscribe({
      next: () => {
        Swal.fire('Success', this.isEditMode ? 'FAQ updated successfully.' : 'FAQ created successfully.', 'success');
        this.resetForm();
      },
      error: () =>
        Swal.fire('Error', 'Failed to save FAQ. Please try again.', 'error'),
    });
  }
  
  // Reset the form
  resetForm(): void {
    this.faqForm.reset({ faqId: 0 });
    this.isEditMode = false;
  }
}