import { TestBed } from '@angular/core/testing';

import { AccountsAccessService } from './accounts-access.service';

describe('AccountsAccessService', () => {
  let service: AccountsAccessService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AccountsAccessService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
