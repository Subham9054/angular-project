import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ComplaintregistrationComponent } from './complaintregistration.component';

describe('ComplaintregistrationComponent', () => {
  let component: ComplaintregistrationComponent;
  let fixture: ComponentFixture<ComplaintregistrationComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ComplaintregistrationComponent]
    });
    fixture = TestBed.createComponent(ComplaintregistrationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
