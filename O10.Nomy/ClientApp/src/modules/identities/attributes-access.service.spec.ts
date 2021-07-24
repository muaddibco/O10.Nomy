import { TestBed } from '@angular/core/testing';

import { AttributesAccessService } from './attributes-access.service';

describe('AttributesServiceService', () => {
  let service: AttributesAccessService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AttributesAccessService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
