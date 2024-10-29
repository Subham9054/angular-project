import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ComplaintcategoryComponent } from './complaintcategory.component';

describe('ComplaintcategoryComponent', () => {
  let component: ComplaintcategoryComponent;
  let fixture: ComponentFixture<ComplaintcategoryComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ComplaintcategoryComponent]
    });
    fixture = TestBed.createComponent(ComplaintcategoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
