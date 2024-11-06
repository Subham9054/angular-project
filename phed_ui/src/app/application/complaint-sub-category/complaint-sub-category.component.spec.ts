import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ComplaintSubCategoryComponent } from './complaint-sub-category.component';

describe('ComplaintSubCategoryComponent', () => {
  let component: ComplaintSubCategoryComponent;
  let fixture: ComponentFixture<ComplaintSubCategoryComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ComplaintSubCategoryComponent]
    });
    fixture = TestBed.createComponent(ComplaintSubCategoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
