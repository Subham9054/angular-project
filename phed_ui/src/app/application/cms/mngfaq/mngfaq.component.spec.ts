import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MngfaqComponent } from './mngfaq.component';

describe('MngfaqComponent', () => {
  let component: MngfaqComponent;
  let fixture: ComponentFixture<MngfaqComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [MngfaqComponent]
    });
    fixture = TestBed.createComponent(MngfaqComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
