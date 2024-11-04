import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MngmenuComponent } from './mngmenu.component';

describe('MngmenuComponent', () => {
  let component: MngmenuComponent;
  let fixture: ComponentFixture<MngmenuComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [MngmenuComponent]
    });
    fixture = TestBed.createComponent(MngmenuComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
