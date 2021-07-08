import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Account } from './models/account'

@Injectable({
  providedIn: 'root'
})
export class AccountsAccessService {

  constructor(private http: HttpClient) { }

  getAccountById(accountId: number) {
    return this.http.get<Account>('/api/accounts/' + accountId);
  }

  find(alias: string) {
    return this.http.get<Account>('/api/accounts/find', { params: { "accountAlias": alias } })
  }
}
