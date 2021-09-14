import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UserDto} from '../accounts/models/account';
import { QrCodeDto } from '../joint-purchases/joint-purchases.service';
import { ActionDetails } from './models/action-details';
import { ActionInfo } from './models/action-info';
import { InvoiceEntry } from './models/invoice-entry';
import { UnauthorizedUse } from './models/unauthorized-use';
import { UniversalProofsRequest } from './models/universal-proofs-request';
import { User } from './models/user';
import { UserAccountDetails } from './models/user-account-details';
import { UserAttributeScheme } from './models/user-attribute-scheme';
import { UserDetails } from './models/user-details';

@Injectable({
  providedIn: 'root'
})
export class UserAccessService {

  constructor(private http: HttpClient) { }

  getO10HubUri() {
    return this.http.get<Map<string, string>>('/api/JointService/O10Hub');
  }

  register(user: User) {
    return this.http.post<UserDto>('/api/accounts', user)
  }

  getUserAttributes(accountId: number) {
    return this.http.get<UserAttributeScheme[]>('/api/user/' + accountId + '/attributes')
  }

  getUserDetails(accountId: number) {
    return this.http.get<UserDetails>('/api/user/' + accountId)
  }

  getUserAccountDetails(accountId: number) {
    return this.http.get<UserAccountDetails>('/api/user/' + accountId + '/details')
  }

  sendCompromizationClaim(accountId: number, unauthorizedUse: UnauthorizedUse) {
    return this.http.post('/api/user/' + accountId + '/compromized', unauthorizedUse)
  }

  confirmSession(sessionId: string) {
    return this.http.post('/api/user/session/' + sessionId, null)
  }

  sendInvoice(accountId: number, sessionId: string, amount: number, currency: string) {
    return this.http.post<InvoiceEntry>('/api/user/' + accountId + '/invoice', { sessionId, amount, currency })
  }

  getActionInfo(actionInfo: string) {
    return this.http.get<ActionInfo>('/api/user/ActionInfo', { params: { actionInfo: actionInfo } });
  }

  getActionDetails(accountId: number, actionInfo: string, attributeId: number) {
    return this.http.get<ActionDetails>('/api/user/' + accountId + '/ActionDetails', { params: { actionInfo: actionInfo, userAttributeId: attributeId } })
  }

  sendUniversalProofs(accountId: number, universalProofsRequest: UniversalProofsRequest) {
    return this.http.post('/api/user/' + accountId + '/UniversalProofs', universalProofsRequest);
  }

  getDisclosedSecrets(accountId: number, password: string) {
    return this.http.get<QrCodeDto>('/api/user/' + accountId + '/secrets?password=' + password)
  }
}
