export interface UserAccountDetails {
  id: number;
  accountInfo: string;
  publicSpendKey: string;
  publicViewKey: string;
  isCompromised: boolean;
  isAutoTheftProtection: boolean;
  consentManagementHub: string;
}
