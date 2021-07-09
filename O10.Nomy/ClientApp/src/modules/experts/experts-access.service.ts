import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ExpertProfile } from './models/expert-profile';
import { ExpertiseArea } from './models/expertise-area';
import { PaymentRecordEntry } from './models/payment-record-entry';
import { SessionInfo } from './models/session-info';

@Injectable({
  providedIn: 'root'
})
export class ExpertsAccessService {

  constructor(private http: HttpClient) { }

  getAllExpertiseAreas() {
    return this.http.get<ExpertiseArea[]>('/api/experts')
  }

  getExpert(expertProfileId: number) {
    return this.http.get<ExpertProfile>('/api/experts/' + expertProfileId)
  }

  initiateChatSession() {
    return this.http.post<SessionInfo>('/api/experts/session', null)
  }

  startChat(expertProfileId: number, sessionId: string) {
    return this.http.post<SessionInfo>('/api/experts/' + expertProfileId + '/session/' + sessionId, null)
  }

  inviteToChat(expertProfileId: number, sessionId: string) {
    return this.http.post('/api/experts/' + expertProfileId + '/chat', null, {
      params: {
        sessionId: sessionId
      }
    })
  }

  payInvoice(userId: number, sessionId: string, invoiceCommitment: string, currency: string, amount: number) {
    return this.http.post<PaymentRecordEntry>('/api/user/' + userId + "/pay", { sessionId, invoiceCommitment, currency, amount });
  }
}
