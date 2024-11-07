import { Component } from '@angular/core';
declare var $: any;
@Component({
  selector: 'app-gallerydetails',
  templateUrl: './gallerydetails.component.html',
  styleUrls: ['./gallerydetails.component.scss']
})
export class GallerydetailsComponent {
  ngOnInit(): void {
    setTimeout(() => {
      $('#photogallery , #videogallery').lightGallery();
    }, 2000);
  }
}
