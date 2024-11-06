import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MngfaqViewComponent } from './mngfaq-view.component';

describe('MngfaqViewComponent', () => {
  let component: MngfaqViewComponent;
  let fixture: ComponentFixture<MngfaqViewComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [MngfaqViewComponent]
    });
    fixture = TestBed.createComponent(MngfaqViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
