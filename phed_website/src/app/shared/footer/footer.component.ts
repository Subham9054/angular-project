import { AfterViewInit, Component, ElementRef } from '@angular/core';

declare var bootstrap: any;

@Component({
  selector: 'app-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.scss']
})
export class FooterComponent implements AfterViewInit {
  isShow: boolean = false;

  constructor(private elementRef: ElementRef) {
    // Show the button when the user scrolls down
    window.onscroll = () => {
      this.isShow = document.documentElement.scrollTop > 100; // Change 100 to your desired scroll position
    };
  }

  ngAfterViewInit() {
    const tooltipTriggerList = [].slice.call(
      this.elementRef.nativeElement.querySelectorAll('[data-bs-toggle="tooltip"]')
    );

    // Initialize tooltips
    tooltipTriggerList.forEach((tooltipTriggerEl: HTMLElement) => {
      const tooltipInstance = new bootstrap.Tooltip(tooltipTriggerEl, {
        trigger: 'hover',
      });
      tooltipTriggerEl.addEventListener('click', () => {
        tooltipInstance.hide();
      });
    });
  }

  gotoTop() {
    window.scrollTo({ top: 0, behavior: 'smooth' });
  }
}