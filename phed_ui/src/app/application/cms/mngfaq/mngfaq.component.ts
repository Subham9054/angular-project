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
  styleUrls: ['./mngfaq.component.scss']
})
export class MngfaqComponent implements OnInit {
  editor = ClassicEditor;
  faqForm: FormGroup;
  faqs: FAQ[] = [];
  selectedFaq: FAQ | null = null;
  isEditMode = false;
  faqAnsEngData: string = `<p class="text-grey">Enter here...</p>`;
  faqAnsHinData: string = `<p class="text-grey">Enter here...</p>`;

  constructor(private fb: FormBuilder, private authService: AuthService, private route: ActivatedRoute) {
    this.faqForm = this.fb.group({
      faqEng: ['', [Validators.required, Validators.maxLength(200)]],
      faqHin: ['', [Validators.required, Validators.maxLength(200)]],
      faqAnsEng: ['', [Validators.required, Validators.maxLength(500)]],
      faqAnsHin: ['', [Validators.required, Validators.maxLength(500)]]
    });
  }

  ngOnInit(): void {
    this.checkForEdit(); // Check if editing a specific FAQ
  }

  // Check route params for edit mode
  checkForEdit(): void {
    this.route.params.subscribe(params => {
      const id = params['id'];
      if (id) {
        const faqId = parseInt(id, 10);
        if (!isNaN(faqId)) {
          this.isEditMode = true;
          this.authService.getFAQById(faqId).subscribe({
            next: (faq: FAQ) => this.populateForm(faq),
            error: () => Swal.fire('Error', 'Failed to load FAQ for editing', 'error')
          });
        }
      }
    });
  }

  // Populate form for editing
  populateForm(faq: FAQ): void {
    this.selectedFaq = faq;
    this.faqForm.patchValue({
      faqEng: faq.faqEng,
      faqHin: faq.faqHin,
      faqAnsEng: faq.faqAnsEng,
      faqAnsHin: faq.faqAnsHin
    });
    this.faqAnsEngData = faq.faqAnsEng;
    this.faqAnsHinData = faq.faqAnsHin;
  }

  // Handle form submission for creating or updating an FAQ
  onSubmit(): void {
    if (this.faqForm.invalid) {
      Swal.fire('Validation Error', 'Please fill all required fields correctly.', 'warning');
      return;
    }

    const faqData: FAQ = {
      faqId: this.isEditMode && this.selectedFaq ? this.selectedFaq.faqId : 0,
      faqEng: this.faqForm.value.faqEng,
      faqHin: this.faqForm.value.faqHin,
      faqAnsEng: this.faqForm.value.faqAnsEng,
      faqAnsHin: this.faqForm.value.faqAnsHin,
      createdBy: 1 // Assuming a static value for createdBy
    };

    this.authService.createOrUpdateFAQ(faqData).subscribe({
      next: () => {
        Swal.fire('Success', this.isEditMode ? 'FAQ updated successfully.' : 'FAQ created successfully.', 'success');
        this.resetForm(); // Reset the form after submission
      },
      error: () => Swal.fire('Error', 'Failed to save FAQ', 'error')
    });
  }

  // Reset form and switch back to create mode
  resetForm(): void {
    this.faqForm.reset();
    this.isEditMode = false;
    this.selectedFaq = null;
    this.faqAnsEngData = `<p class="text-grey">Enter here...</p>`; // Reset CKEditor content for English
    this.faqAnsHinData = `<p class="text-grey">Enter here...</p>`; // Reset CKEditor content for Hindi
  }
}