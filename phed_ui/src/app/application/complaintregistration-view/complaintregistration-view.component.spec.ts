import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ComplaintregistrationViewComponent } from './complaintregistration-view.component';

describe('ComplaintregistrationViewComponent', () => {
  let component: ComplaintregistrationViewComponent;
  let fixture: ComponentFixture<ComplaintregistrationViewComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ComplaintregistrationViewComponent]
    });
    fixture = TestBed.createComponent(ComplaintregistrationViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
