import { TestBed } from '@angular/core/testing';

import { ValidatorChecklistService } from './validator-checklist.service';

describe('ValidatorChecklistService', () => {
  let service: ValidatorChecklistService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ValidatorChecklistService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
