import { Component } from '@angular/core';
import Swiper from 'swiper';
import { Navigation, Pagination, Autoplay } from 'swiper'; // Import Navigation and Pagination correctly
Swiper.use([Navigation, Pagination, Autoplay]);
declare var $: any;

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {
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

  ngOnInit(): void {
    setTimeout(() => {
      $('#photogallery , #videogallery').lightGallery();
    }, 2000);
  }
  
}
