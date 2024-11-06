import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MinisterProfileComponent } from './minister-profile.component';

describe('MinisterProfileComponent', () => {
  let component: MinisterProfileComponent;
  let fixture: ComponentFixture<MinisterProfileComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [MinisterProfileComponent]
    });
    fixture = TestBed.createComponent(MinisterProfileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
