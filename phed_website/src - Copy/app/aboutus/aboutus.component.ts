import { Component, OnInit } from '@angular/core';
import { HomeService } from '../services/home.service';
import { TranslationService } from '../services/translation.service';

@Component({
  selector: 'app-aboutus',
  templateUrl: './aboutus.component.html',
  styleUrls: ['./aboutus.component.scss']
})
export class AboutusComponent implements OnInit {
  aboutUsContent: any[] = [];
  currentLanguage!: string;

  constructor(private homeService: HomeService, private translationService: TranslationService) { }

  ngOnInit(): void {
    // Set current language based on the translation service
    this.currentLanguage = this.translationService.getLanguage(); 

    // Listen for language change and update accordingly
    this.translationService.onLanguageChange().subscribe((event) => {
      this.currentLanguage = event.lang;
      this.aboutUs();
    });

    // Initial fetch of About Us content
    this.aboutUs();
  }

  // Fetch About Us content from the API
  aboutUs() {
    this.homeService.aboutUs().subscribe({
        next: (response) => {
            if (response.success) {
              this.aboutUsContent = response.data;
            } else {
                console.error('Error fetching About Us content');
              }
        },
        error: (err) => {
            console.error('API error:', err);
        }
    });
  }

  // Toggle the current language
  toggleLanguage() {
    this.currentLanguage = this.currentLanguage === 'en' ? 'hi' : 'en';
    this.translationService.setLanguage(this.currentLanguage);
  }
}
