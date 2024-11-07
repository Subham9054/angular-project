import { Component, OnInit } from '@angular/core';
import { HomeService } from '../services/home.service';
import { TranslationService } from '../services/translation.service';

@Component({
  selector: 'app-contact-us',
  templateUrl: './contact-us.component.html',
  styleUrls: ['./contact-us.component.scss']
})
export class ContactUsComponent implements OnInit {
  contactDetails: any[] = [];
  currentLanguage!: string;

  constructor(private homeService: HomeService, private translationService: TranslationService) { }

  ngOnInit(): void {
    // Set current language based on the translation service
    this.currentLanguage = this.translationService.getLanguage(); 

    // Listen for language change and update accordingly
    this.translationService.onLanguageChange().subscribe((event) => {
      this.currentLanguage = event.lang;
      this.getContactDetails();
    });

    // Initial fetch of contact details
    this.getContactDetails();
  }

  // Fetch contact details from the API
  getContactDetails() {
    this.homeService.contactDetails().subscribe({
      next: (response) => {
        if (response.success) {
          this.contactDetails = response.data;
        } else {
          console.error('Error fetching contact details');
        }
      },
      error: (err) => {
        console.error('API error:', err);
      },
      complete: () => {
        console.log('Contact details retrieval completed');
      }
    });
  }

  // Toggle the current language
  toggleLanguage() {
    this.currentLanguage = this.currentLanguage === 'en' ? 'hi' : 'en';
    this.translationService.setLanguage(this.currentLanguage);
  }
}
