import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WhatnewViewComponent } from './whatnew-view.component';

describe('WhatnewViewComponent', () => {
  let component: WhatnewViewComponent;
  let fixture: ComponentFixture<WhatnewViewComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [WhatnewViewComponent]
    });
    fixture = TestBed.createComponent(WhatnewViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
