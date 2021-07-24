import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AttributeScheme } from './models/attribute-scheme';

@Injectable({
  providedIn: 'root'
})
export class AttributesAccessService {

  constructor(private http: HttpClient) { }

  getUserAttributes(accountId: number) {
    return this.http.get<AttributeScheme[]>('/api/user/' + accountId + '/attributes')
  }
}
