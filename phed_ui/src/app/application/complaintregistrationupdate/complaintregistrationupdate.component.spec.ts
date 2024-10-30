import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ComplaintregistrationupdateComponent } from './complaintregistrationupdate.component';

describe('ComplaintregistrationupdateComponent', () => {
  let component: ComplaintregistrationupdateComponent;
  let fixture: ComponentFixture<ComplaintregistrationupdateComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ComplaintregistrationupdateComponent]
    });
    fixture = TestBed.createComponent(ComplaintregistrationupdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
