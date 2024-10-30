import { Component, AfterViewInit, ElementRef } from '@angular/core';
import { VisitorService } from 'src/app/services/visitor.service';
import { TranslationService } from 'src/app/services/translation.service';
import { HomeService } from 'src/app/services/home.service';

declare var bootstrap: any;
@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.scss']
})
export class FooterComponent  implements AfterViewInit{
  visitorCount: number = 0;
  isShow: boolean = false;
  private elementRef: ElementRef | undefined;

  constructor(
    private homeService: HomeService,
    private translate: TranslationService,
    private visitorService: VisitorService, 
    elementRef: ElementRef) {
    this.elementRef = elementRef;

    // Show the button when the user scrolls down
    window.onscroll = () => {
      this.isShow = document.documentElement.scrollTop > 100; // Change 100 to your desired scroll position
    };
  }

  ngOnInit(): void {
    // Increment the visitor count on page load
    this.visitorService.incrementVisitorCount().subscribe(() => {
      this.loadVisitorCount();
    });
  } 

  // Load visitor count from API
  loadVisitorCount(): void {
    this.visitorService.getVisitorCount().subscribe(visitorCount => {
      this.visitorCount = visitorCount; // Assign the count value
    });
  }

  ngAfterViewInit() {
    const tooltips = this.elementRef!.nativeElement.querySelectorAll('[data-bs-toggle="tooltip"]');
    if (tooltips.length) {
      tooltips.forEach((tooltip: HTMLElement) => {
        // Initialize your tooltips here
      });
    }
  }

  gotoTop() {
    window.scrollTo({ top: 0, behavior: 'smooth' });
  }

  // Function to convert numbers to Hindi numerals
  convertToHindiNumerals(num: number): string {
    const hindiNumerals: { [key: string]: string } = {
      '0': '०',
      '1': '१',
      '2': '२',
      '3': '३',
      '4': '४',
      '5': '५',
      '6': '६',
      '7': '७',
      '8': '८',
      '9': '९'
    };

    return num.toString().split('').map(digit => hindiNumerals[digit] || digit).join('');
  }

  // Function to get the translated visitors string
  getTranslatedVisitors(): string {
    const currentLang = this.translate.getLanguage(); // Use the getLanguage method to get the current language

    if (currentLang === 'hi') {
      return this.convertToHindiNumerals(this.visitorCount); // Return Hindi numeral representation
    } else {
      return this.visitorCount.toString(); // Return as string for English
    }
  }
}