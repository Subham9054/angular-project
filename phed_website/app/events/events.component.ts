import { AfterViewInit, Component, ElementRef } from '@angular/core';
declare var bootstrap: any;
import { Location } from '@angular/common';
@Component({
  selector: 'app-events',
  templateUrl: './events.component.html',
  styleUrls: ['./events.component.scss']
})
export class EventsComponent implements AfterViewInit {
 
  private elementRef: ElementRef;
  
  constructor(elementRef: ElementRef, private location: Location) {
    this.elementRef = elementRef;
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
  goBack() {
    this.location.back(); // Navigate back to the previous page
  }
}
