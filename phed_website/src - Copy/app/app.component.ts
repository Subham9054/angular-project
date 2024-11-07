import { Component, HostListener } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  paddingTop = '7.5rem'; // Default padding
  title = ' NEER NIRMAL SEVA (PHED)';
  previousScroll = 0;
  headerVisible = true;
  @HostListener('window:scroll', [])
  onWindowScroll() {
    const currentScroll = window.scrollY;

    // Hide header on scroll down, show on scroll up
    if (currentScroll > this.previousScroll) {
      this.headerVisible = false; // Scrolling down
      
    } else {
      this.headerVisible = true; // Scrolling up
    }
 // Set padding based on scroll position
 if (currentScroll > 0 && this.paddingTop !== '0') {
  this.paddingTop = '0'; // Set padding to 0 when scrolling down
} else if (currentScroll === 0 && this.paddingTop !== '8.5rem') {
  this.paddingTop = '7.5rem'; // Reset padding when at the top
}
    this.previousScroll = currentScroll; // Update previous scroll position
  }
}
