import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PagecontentViewComponent } from './pagecontent-view.component';

describe('PagecontentViewComponent', () => {
  let component: PagecontentViewComponent;
  let fixture: ComponentFixture<PagecontentViewComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PagecontentViewComponent]
    });
    fixture = TestBed.createComponent(PagecontentViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
