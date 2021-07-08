export interface PaymentEntry {
  sessionId: string;
  commitment: string;
  currency: string;
  amount: number;
}
