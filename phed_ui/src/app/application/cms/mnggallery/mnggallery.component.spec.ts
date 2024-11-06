import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MnggalleryComponent } from './mnggallery.component';

describe('MnggalleryComponent', () => {
  let component: MnggalleryComponent;
  let fixture: ComponentFixture<MnggalleryComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [MnggalleryComponent]
    });
    fixture = TestBed.createComponent(MnggalleryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
