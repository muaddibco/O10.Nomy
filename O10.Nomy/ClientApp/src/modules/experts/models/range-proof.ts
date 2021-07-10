export interface RangeProof {
  d: string[];
  borromeanRingSignature: BorromeanRingSignature;
}

export interface BorromeanRingSignature {
  e: string;
  s: string[][];
}
