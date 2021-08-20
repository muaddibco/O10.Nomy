import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class JointPurchasesService {

  constructor(private http: HttpClient) { }

  getJointServiceAccount() {
    return this.http.get<Account>('/api/JointService');
  }

  getQrCode(id: number) {
    return this.http.get<QrCodeDto>('/api/ServiceProviders/SessionInfo', { params: { accountId: id }});
  }

  getO10HubUri() {
    return this.http.get<Map<string, string>>('/api/JointService/O10Hub');
  }
}

export interface QrCodeDto {
  code: string;
  sessionKey: string;
}

export interface Account {
  accountId: number
}
