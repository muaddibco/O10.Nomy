import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { group } from '@angular/animations';

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

  addJointGroup(o10RegistrationId: number, name: string, description: string) {
    return this.http.post<JointGroup>('/api/JointService/' + o10RegistrationId + '/JointGroup', { name, description });
  }

  getJointGroups(o10RegistrationId: number) {
    return this.http.get<JointGroup[]>('/api/JointService/' + o10RegistrationId + '/JointGroups');
  }

  getJointGroupMembers(groupId: number) {
    return this.http.get<JointGroupMember[]>('/api/JointService/JointGroup/' + groupId + '/Members');
  }

  addJointGroupMember(groupId: number, email: string, description: string) {
    return this.http.post<JointGroupMember>('/api/JointService/JointGroup/' + groupId + '/Members', { email, description });
  }
}

export interface QrCodeDto {
  code: string;
  sessionKey: string;
}

export interface Account {
  accountId: number
}

export interface JointGroup {
  jointGroupId: number
  o10RegistrationId: number
  name: string
  description: string
}

export interface JointGroupMember {
  jointGroupMemberId: number
  email: string
  description: string
}
