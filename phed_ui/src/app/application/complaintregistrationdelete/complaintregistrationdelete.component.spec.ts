import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ComplaintregistrationdeleteComponent } from './complaintregistrationdelete.component';

describe('ComplaintregistrationdeleteComponent', () => {
  let component: ComplaintregistrationdeleteComponent;
  let fixture: ComponentFixture<ComplaintregistrationdeleteComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ComplaintregistrationdeleteComponent]
    });
    fixture = TestBed.createComponent(ComplaintregistrationdeleteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
