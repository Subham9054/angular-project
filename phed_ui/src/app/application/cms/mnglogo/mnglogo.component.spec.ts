import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MnglogoComponent } from './mnglogo.component';

describe('MnglogoComponent', () => {
  let component: MnglogoComponent;
  let fixture: ComponentFixture<MnglogoComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [MnglogoComponent]
    });
    fixture = TestBed.createComponent(MnglogoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
