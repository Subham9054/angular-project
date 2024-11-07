import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GallerydetailsComponent } from './gallerydetails.component';

describe('GallerydetailsComponent', () => {
  let component: GallerydetailsComponent;
  let fixture: ComponentFixture<GallerydetailsComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [GallerydetailsComponent]
    });
    fixture = TestBed.createComponent(GallerydetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
