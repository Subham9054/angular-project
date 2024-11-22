import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../auth.service';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';
import { TranslationService } from '../services/translation.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-faq',
  templateUrl: './faq.component.html',
  styleUrls: ['./faq.component.scss']
})
export class FaqComponent implements OnInit {
  faqs: any[] = [];
  isLoading = true;
  errorMessage = '';
  activeIndex: number | null = null; // Tracks the currently active FAQ accordion
  noRecordsFound = false;
  currentLanguage: string = 'en'; // Default language (English)

  constructor(
    private authService: AuthService,
    private router: Router,
    private sanitizer: DomSanitizer,
    private translationService: TranslationService
  ) {}

  ngOnInit(): void {
    // Load FAQs on component initialization
    this.loadFAQs();

    // Listen to language change events from TranslationService
    this.translationService.onLanguageChange().subscribe((event) => {
      this.currentLanguage = event.lang;
      this.updateFAQsForLanguage();
    });

    // Set the initial language
    this.currentLanguage = this.translationService.getLanguage();
  }

  // Method to load FAQs
  loadFAQs(): void {
    this.authService.getFAQs().subscribe({
      next: (response: any) => {
        this.isLoading = false;
        if (response.success) {
          // Sanitize and store FAQs
          this.faqs = response.data.map((faq: any) => ({
            ...faq,
            faqAnsEng: this.sanitizeHtml(faq.faqAnsEng),
            faqAnsHin: this.sanitizeHtml(faq.faqAnsHin)
          }));
          this.updateFAQsForLanguage(); // Ensure language-specific content is displayed
          this.noRecordsFound = this.faqs.length === 0;
        } else {
          this.noRecordsFound = true;
          Swal.fire(
            this.translationService.instant('info.title'),
            this.translationService.instant('info.noFaqsFound'),
            'info'
          );
        }
      },
      error: (error) => {
        this.isLoading = false;
        this.errorMessage = this.translationService.instant('error.fetchingFaqs');
        console.error('Error fetching FAQs:', error);
        Swal.fire(
          this.translationService.instant('error.title'),
          this.translationService.instant('error.fetchingFaqs'),
          'error'
        );
      }
    });
  }

  // Sanitize HTML for secure rendering
  sanitizeHtml(html: string): SafeHtml {
    return this.sanitizer.bypassSecurityTrustHtml(html);
  }

  // Toggle FAQ visibility
  toggleFAQ(index: number): void {
    this.activeIndex = this.activeIndex === index ? null : index;
  }

  // Update FAQs based on selected language
  updateFAQsForLanguage(): void {
    this.faqs = this.faqs.map((faq) => ({
      ...faq,
      faqEng: this.currentLanguage === 'en' ? faq.faqEng : faq.faqHin,
      faqAnsEng: this.currentLanguage === 'en' ? faq.faqAnsEng : faq.faqAnsHin
    }));
  }
}