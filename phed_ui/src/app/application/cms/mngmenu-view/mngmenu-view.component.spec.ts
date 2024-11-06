import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MngmenuViewComponent } from './mngmenu-view.component';

describe('MngmenuViewComponent', () => {
  let component: MngmenuViewComponent;
  let fixture: ComponentFixture<MngmenuViewComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [MngmenuViewComponent]
    });
    fixture = TestBed.createComponent(MngmenuViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
