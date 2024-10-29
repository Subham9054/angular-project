import { TestBed } from '@angular/core/testing';

import { CommonconfigService } from './commonconfig.service';

describe('CommonconfigService', () => {
  let service: CommonconfigService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CommonconfigService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
