import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegistrationcomplaintComponent } from './registrationcomplaint.component';

describe('RegistrationcomplaintComponent', () => {
  let component: RegistrationcomplaintComponent;
  let fixture: ComponentFixture<RegistrationcomplaintComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [RegistrationcomplaintComponent]
    });
    fixture = TestBed.createComponent(RegistrationcomplaintComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
