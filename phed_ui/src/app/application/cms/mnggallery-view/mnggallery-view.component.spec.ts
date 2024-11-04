import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MnggalleryViewComponent } from './mnggallery-view.component';

describe('MnggalleryViewComponent', () => {
  let component: MnggalleryViewComponent;
  let fixture: ComponentFixture<MnggalleryViewComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [MnggalleryViewComponent]
    });
    fixture = TestBed.createComponent(MnggalleryViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
