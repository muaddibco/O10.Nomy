import { IdentityPool } from "./identity-pool";
import { UniversalProofsMission } from "./universal-proofs-mission";

export interface UniversalProofsRequest {
  rootAttributeId: number;
  target: string;
  sessionKey: string;
  mission: UniversalProofsMission;
  serviceProviderInfo: string;
  identityPools: IdentityPool[];
}
