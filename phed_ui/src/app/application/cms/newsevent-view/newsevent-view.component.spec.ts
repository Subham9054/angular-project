import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NewseventViewComponent } from './newsevent-view.component';

describe('NewseventViewComponent', () => {
  let component: NewseventViewComponent;
  let fixture: ComponentFixture<NewseventViewComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [NewseventViewComponent]
    });
    fixture = TestBed.createComponent(NewseventViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
