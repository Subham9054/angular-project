import { Component, OnInit } from '@angular/core';
import Swiper from 'swiper';
import { Navigation, Pagination, Autoplay } from 'swiper'; // Import Navigation and Pagination correctly
Swiper.use([Navigation, Pagination, Autoplay]);
declare var $: any;

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit{
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
  private stats: { [key: string]: number } = {
    totalComplaints: this.totalComplaints,
    closedComplaints: this.closedComplaints,
    activeComplaints: this.activeComplaints
  };
  ngOnInit(): void {
    this.animateNumber('totalComplaints', 857000);
    this.animateNumber('closedComplaints', 733000);
    this.animateNumber('activeComplaints', 124000);
    this.averageResolutionTime = 3; // Static value for average time
    setTimeout(() => {
      $('#photogallery , #videogallery').lightGallery();
    }, 2000);
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
