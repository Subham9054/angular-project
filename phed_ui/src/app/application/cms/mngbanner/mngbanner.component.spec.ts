import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MngbannerComponent } from './mngbanner.component';

describe('MngbannerComponent', () => {
  let component: MngbannerComponent;
  let fixture: ComponentFixture<MngbannerComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [MngbannerComponent]
    });
    fixture = TestBed.createComponent(MngbannerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
