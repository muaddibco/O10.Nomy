import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UserDto } from './models/account'
import { DisclosedSecrets } from './models/disclosed-secrets';

@Injectable({
  providedIn: 'root'
})
export class AccountsAccessService {

  constructor(private http: HttpClient) { }

  getAccountById(accountId: number) {
    return this.http.get<UserDto>('/api/accounts/' + accountId);
  }

  find(alias: string) {
    return this.http.get<UserDto>('/api/accounts/find', { params: { "accountAlias": alias } })
  }

  authenticate(accountId: number, password: string) {
    return this.http.post('/api/accounts/' + accountId + '/auth', { password })
  }

  override(accountId: number, disclosedSecrets: DisclosedSecrets) {
    return this.http.put('/api/accounts/' + accountId, disclosedSecrets)
  }

  isAuthenticated(accountId: number) {
    return this.http.get<IsAuthenticated>('/api/accounts/' + accountId + '/auth')
  }

  reset(accountId: number, password: string) {
    return this.http.post('/api/accounts/' + accountId + '/reset', { password })
  }
}

export interface IsAuthenticated {
  isAuthenticated: boolean
}
