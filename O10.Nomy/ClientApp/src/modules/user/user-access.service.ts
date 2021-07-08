import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { filter } from 'rxjs/operators';
import { AccountsAccessService } from '../accounts/accounts-access.service';
import { AccountType } from '../accounts/models/account-type';
import { Account} from '../accounts/models/account';
import { User } from './models/user';
import { UserAttributeScheme } from './models/user-attribute-scheme';
import { UserDetails } from './models/user-details';

@Injectable({
  providedIn: 'root'
})
export class UserAccessService {

  constructor(private http: HttpClient, private accountsAccessService: AccountsAccessService) { }

  register(user: User) {
    return this.http.post<Account>('/api/accounts', user)
  }

  getUserAttributes(account: Account) {
    return this.http.get<UserAttributeScheme[]>('/api/user/' + account.accountId + '/attributes')
  }

  getUserDetails(account: Account) {
    return this.http.get<UserDetails>('/api/user/' + account.accountId)
  }

  confirmSession(sessionId: string) {
    return this.http.post('/api/user/session/' + sessionId, null)
  }
}
