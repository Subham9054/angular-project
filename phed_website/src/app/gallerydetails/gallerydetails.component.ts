import { AfterViewInit, Component, ElementRef } from '@angular/core';
import { Location } from '@angular/common';
declare var $: any;
declare var bootstrap: any;

@Component({
  selector: 'app-gallerydetails',
  templateUrl: './gallerydetails.component.html',
  styleUrls: ['./gallerydetails.component.scss']
})
export class GallerydetailsComponent implements AfterViewInit {
  private elementRef: ElementRef; // Declare elementRef

  constructor(elementRef: ElementRef, private location: Location) {
    this.elementRef = elementRef; // Initialize elementRef
  }

  ngOnInit(): void {
    setTimeout(() => {
      $('#photogallery, #videogallery').lightGallery();
    }, 2000);
  }

  ngAfterViewInit() {
    setTimeout(() => {
      const tooltipTriggerList = [].slice.call(
        this.elementRef.nativeElement.querySelectorAll('[data-bs-toggle="tooltip"]')
      );
      const tooltipList = tooltipTriggerList.map((tooltipTriggerEl: Element) => {
        const tooltipInstance = new bootstrap.Tooltip(tooltipTriggerEl, {
          trigger: 'hover',
        });
        tooltipTriggerEl.addEventListener('click', () => {
          tooltipInstance.hide();
        });
        return tooltipInstance;
      });
    });
  }

  goBack() {
    this.location.back(); // Navigate back to the previous page
  }
}
