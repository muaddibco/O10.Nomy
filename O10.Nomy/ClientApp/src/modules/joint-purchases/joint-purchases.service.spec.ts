import { TestBed } from '@angular/core/testing';

import { JointPurchasesService } from './joint-purchases.service';

describe('JointPurchasesService', () => {
  let service: JointPurchasesService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(JointPurchasesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
