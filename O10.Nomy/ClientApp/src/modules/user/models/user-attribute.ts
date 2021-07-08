import { AttributeState } from "./attribute-state";

export interface UserAttribute {
  userAttributeId: number;
  schemeName: string;
  source: string;
  issuerName: string;
  validated: boolean;
  content: string;
  state: AttributeState;
  isOverriden: boolean;
}
