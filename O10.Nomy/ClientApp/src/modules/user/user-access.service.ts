import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Account} from '../accounts/models/account';
import { InvoiceEntry } from './models/invoice-entry';
import { User } from './models/user';
import { UserAttributeScheme } from './models/user-attribute-scheme';
import { UserDetails } from './models/user-details';

@Injectable({
  providedIn: 'root'
})
export class UserAccessService {

  constructor(private http: HttpClient) { }

  register(user: User) {
    return this.http.post<Account>('/api/accounts', user)
  }

  getUserAttributes(accountId: number) {
    return this.http.get<UserAttributeScheme[]>('/api/user/' + accountId + '/attributes')
  }

  getUserDetails(accountId: number) {
    return this.http.get<UserDetails>('/api/user/' + accountId)
  }

  confirmSession(sessionId: string) {
    return this.http.post('/api/user/session/' + sessionId, null)
  }

  sendInvoice(accountId: number, sessionId: string, amount: number, currency: string) {
    return this.http.post<InvoiceEntry>('/api/user/' + accountId + '/invoice', { sessionId, amount, currency })
  }
}
