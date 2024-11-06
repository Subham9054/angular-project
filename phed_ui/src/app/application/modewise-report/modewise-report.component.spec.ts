import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModewiseReportComponent } from './modewise-report.component';

describe('ModewiseReportComponent', () => {
  let component: ModewiseReportComponent;
  let fixture: ComponentFixture<ModewiseReportComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ModewiseReportComponent]
    });
    fixture = TestBed.createComponent(ModewiseReportComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
