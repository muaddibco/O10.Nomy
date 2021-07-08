import { TestBed } from '@angular/core/testing';

import { ExpertsAccessService } from './experts-access.service';

describe('ExpertsAccessService', () => {
  let service: ExpertsAccessService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ExpertsAccessService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
