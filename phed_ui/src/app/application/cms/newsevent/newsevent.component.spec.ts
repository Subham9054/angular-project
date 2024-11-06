import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NewseventComponent } from './newsevent.component';

describe('NewseventComponent', () => {
  let component: NewseventComponent;
  let fixture: ComponentFixture<NewseventComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [NewseventComponent]
    });
    fixture = TestBed.createComponent(NewseventComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
