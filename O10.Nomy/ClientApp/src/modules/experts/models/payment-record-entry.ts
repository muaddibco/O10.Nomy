import { RangeProof } from "./range-proof";

export interface PaymentRecordEntry {
  commitment: string;
  rangeProof: RangeProof;
}
