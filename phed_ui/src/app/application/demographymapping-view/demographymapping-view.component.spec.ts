import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DemographymappingViewComponent } from './demographymapping-view.component';

describe('DemographymappingViewComponent', () => {
  let component: DemographymappingViewComponent;
  let fixture: ComponentFixture<DemographymappingViewComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DemographymappingViewComponent]
    });
    fixture = TestBed.createComponent(DemographymappingViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
