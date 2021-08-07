import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UserDto} from '../accounts/models/account';
import { ActionDetails } from './models/action-details';
import { ActionInfo } from './models/action-info';
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
    return this.http.post<UserDto>('/api/accounts', user)
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

  getActionInfo(actionInfo: string) {
    return this.http.get<ActionInfo>('/api/user/', { params: { actionInfo: actionInfo } });
  }

  getActionDetails(accountId: number, actionInfo: string, attributeId: number) {
    return this.http.get<ActionDetails>('/api/user/' + accountId + '/ActionDetails', { params: { actionInfo: actionInfo, userAttributeId: attributeId } })
  }
}
