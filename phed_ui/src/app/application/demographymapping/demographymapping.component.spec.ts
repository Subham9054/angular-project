import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DemographymappingComponent } from './demographymapping.component';

describe('DemographymappingComponent', () => {
  let component: DemographymappingComponent;
  let fixture: ComponentFixture<DemographymappingComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DemographymappingComponent]
    });
    fixture = TestBed.createComponent(DemographymappingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
