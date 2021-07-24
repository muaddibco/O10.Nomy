import { AttributeState } from "./attribute-state";

export interface RootAttribute {
  userAttributeId: number;
  schemeName: string;
  issuerAddress: string;
  issuerName: string;
  content: string;
  state: AttributeState
}
