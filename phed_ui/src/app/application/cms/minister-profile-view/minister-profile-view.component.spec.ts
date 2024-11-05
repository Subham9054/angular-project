import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MinisterProfileViewComponent } from './minister-profile-view.component';

describe('MinisterProfileViewComponent', () => {
  let component: MinisterProfileViewComponent;
  let fixture: ComponentFixture<MinisterProfileViewComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [MinisterProfileViewComponent]
    });
    fixture = TestBed.createComponent(MinisterProfileViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
