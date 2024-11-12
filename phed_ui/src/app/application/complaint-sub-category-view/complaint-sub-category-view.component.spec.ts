import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ComplaintSubCategoryViewComponent } from './complaint-sub-category-view.component';

describe('ComplaintSubCategoryViewComponent', () => {
  let component: ComplaintSubCategoryViewComponent;
  let fixture: ComponentFixture<ComplaintSubCategoryViewComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ComplaintSubCategoryViewComponent]
    });
    fixture = TestBed.createComponent(ComplaintSubCategoryViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
