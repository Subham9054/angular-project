import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MngcontactComponent } from './mngcontact.component';

describe('MngcontactComponent', () => {
  let component: MngcontactComponent;
  let fixture: ComponentFixture<MngcontactComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [MngcontactComponent]
    });
    fixture = TestBed.createComponent(MngcontactComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
