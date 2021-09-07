import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UserDto } from './models/account'

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

  duplicate(accountId: number, newEmail: string) {
    return this.http.post('/api/accounts/' + accountId + '/duplicate', { newEmail })
  }

  isAuthenticated(accountId: number) {
    return this.http.get<IsAuthenticated>('/api/accounts/' + accountId + '/auth')
  }
}

export interface IsAuthenticated {
  isAuthenticated: boolean
}
