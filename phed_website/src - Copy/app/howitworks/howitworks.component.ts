import { Component, OnInit } from '@angular/core';
import { HomeService } from '../services/home.service';
import { TranslationService } from '../services/translation.service';

@Component({
  selector: 'app-howitworks',
  templateUrl: './howitworks.component.html',
  styleUrls: ['./howitworks.component.scss']
})
export class HowitworksComponent implements OnInit {
  howItWorksContent: any[] = [];
  currentLanguage!: string;

  constructor(private homeService: HomeService, private translationService: TranslationService) { }

  ngOnInit(): void {
    // Set current language based on the translation service
    this.currentLanguage = this.translationService.getLanguage(); 

    // Listen for language change and update accordingly
    this.translationService.onLanguageChange().subscribe((event) => {
      this.currentLanguage = event.lang;
      this.getHowItWorks();
    });

    // Initial fetch of How It Works content
    this.getHowItWorks();
  }

  // Fetch 'How It Works' content from the API
  getHowItWorks() {
    this.homeService.howItWorks().subscribe({
      next: (response) => {
        if (response.success) {
          this.howItWorksContent = response.data;
        } else {
          console.error('Error fetching How It Works content');
        }
      },
      error: (err) => {
        console.error('API error:', err);
      },
      complete: () => {
        console.log('How It Works content retrieval completed');
      }
    });
  }

  // Toggle the current language
  toggleLanguage() {
    this.currentLanguage = this.currentLanguage === 'en' ? 'hi' : 'en';
    this.translationService.setLanguage(this.currentLanguage);
  }

  // Function to convert English numbers to Hindi
  convertNumberToHindi(num: number): string {
    const hindiDigits = ['०', '१', '२', '३', '४', '५', '६', '७', '८', '९'];
    return num.toString().split('').map(digit => hindiDigits[parseInt(digit, 10)]).join('');
  }
}
