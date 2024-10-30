import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PagecontentComponent } from './pagecontent.component';

describe('PagecontentComponent', () => {
  let component: PagecontentComponent;
  let fixture: ComponentFixture<PagecontentComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PagecontentComponent]
    });
    fixture = TestBed.createComponent(PagecontentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
