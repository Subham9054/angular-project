import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MngcontactViewComponent } from './mngcontact-view.component';

describe('MngcontactViewComponent', () => {
  let component: MngcontactViewComponent;
  let fixture: ComponentFixture<MngcontactViewComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [MngcontactViewComponent]
    });
    fixture = TestBed.createComponent(MngcontactViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
