import { Component, OnInit } from '@angular/core';
import Swiper from 'swiper';
import { Navigation, Pagination, Autoplay } from 'swiper'; // Import Navigation and Pagination correctly
import { AuthService } from '../auth.service';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';
import { TranslationService } from '../services/translation.service';
import Swal from 'sweetalert2';
Swiper.use([Navigation, Pagination, Autoplay]);
declare var $: any;

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit{
  faqs: any[] = [];
  displayedFaqs: any[] = []; // FAQs currently displayed based on the selected language
  isLoading = true;
  errorMessage = '';
  activeIndex: number | null = 0; // Default to expand the first FAQ
  noRecordsFound = false;
  currentLanguage: string = 'en'; // Default language (English)

  totalComplaints: number = 0;
  closedComplaints: number = 0;
  activeComplaints: number = 0;
  averageResolutionTime: number = 0;
  ngAfterViewInit() {
    const swiper = new Swiper('.slider-wrap', {
      // loop: true,
      pagination: {
        el: '.swiper-pagination',
        clickable: true,
      },
      // autoplay: {
      //   delay: 3000, // Delay in milliseconds between slides
      //   disableOnInteraction: false, // Continue autoplay after user interactions
      // },
      // navigation: {
      //   nextEl: '.swiper-button-next',
      //   prevEl: '.swiper-button-prev',
      // },
     
    });
  }

  constructor(
    private authService: AuthService,
    private sanitizer: DomSanitizer,
    private translationService: TranslationService
  ) {}

  private stats: { [key: string]: number } = {
    totalComplaints: this.totalComplaints,
    closedComplaints: this.closedComplaints,
    activeComplaints: this.activeComplaints
  };
  ngOnInit(): void {
    // Load FAQs on component initialization
    this.loadFAQs();

    // Listen to language change events from TranslationService
    this.translationService.onLanguageChange().subscribe((event) => {
      this.currentLanguage = event.lang;
      this.updateFAQsForLanguage(); // Update FAQs based on selected language
    });

    // Set the initial language
    this.currentLanguage = this.translationService.getLanguage();
    this.animateNumber('totalComplaints', 857000);
    this.animateNumber('closedComplaints', 733000);
    this.animateNumber('activeComplaints', 124000);
    this.averageResolutionTime = 3; // Static value for average time
    setTimeout(() => {
      $('#photogallery , #videogallery').lightGallery();
    }, 2000);
  }

  // Method to load FAQs
  loadFAQs(): void {
    this.authService.getFAQs().subscribe({
      next: (response: any) => {
        this.isLoading = false;
        if (response.success) {
          // Store FAQs and sanitize their content
          this.faqs = response.data.map((faq: any) => ({
            ...faq,
            faqAnsEng: this.sanitizeHtml(faq.faqAnsEng),
            faqAnsHin: this.sanitizeHtml(faq.faqAnsHin)
          }));
          this.updateFAQsForLanguage(); // Ensure the correct language is displayed
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

  // Update the FAQs displayed based on the selected language
  updateFAQsForLanguage(): void {
    this.displayedFaqs = this.faqs.map((faq) => ({
      ...faq,
      faqEng: this.currentLanguage === 'en' ? faq.faqEng : faq.faqHin,
      faqAnsEng: this.currentLanguage === 'en' ? faq.faqAnsEng : faq.faqAnsHin
    }));
    // Expand the first FAQ after updating language
    this.activeIndex = this.displayedFaqs.length > 0 ? 0 : null;
  }

  animateNumber(property: 'totalComplaints' | 'closedComplaints' | 'activeComplaints', targetValue: number) {
    const duration = 2000; // Total animation duration in ms
    const startValue = this[property];
    const startTime = performance.now();

    const animate = (currentTime: number) => {
      const elapsedTime = currentTime - startTime;
      const progress = Math.min(elapsedTime / duration, 1); // Clamp progress to 0-1
      this[property] = Math.floor(startValue + (targetValue - startValue) * progress);

      if (progress < 1) {
        requestAnimationFrame(animate);
      } else {
        this[property] = targetValue; // Ensure it ends on target value
      }
    };

    requestAnimationFrame(animate);
  }
}