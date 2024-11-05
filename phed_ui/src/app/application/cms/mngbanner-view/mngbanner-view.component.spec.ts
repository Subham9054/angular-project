import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MngbannerViewComponent } from './mngbanner-view.component';

describe('MngbannerViewComponent', () => {
  let component: MngbannerViewComponent;
  let fixture: ComponentFixture<MngbannerViewComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [MngbannerViewComponent]
    });
    fixture = TestBed.createComponent(MngbannerViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
