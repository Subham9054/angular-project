import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ComplaintcategoryViewComponent } from './complaintcategory-view.component';

describe('ComplaintcategoryViewComponent', () => {
  let component: ComplaintcategoryViewComponent;
  let fixture: ComponentFixture<ComplaintcategoryViewComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ComplaintcategoryViewComponent]
    });
    fixture = TestBed.createComponent(ComplaintcategoryViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
