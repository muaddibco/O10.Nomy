import { AttributeState } from "./attribute-state";
import { UserAssociatedAttributes } from "./user-associated-attributes";
import { UserAttribute } from "./user-attribute";

export interface UserAttributeScheme {
  state: AttributeState;
  issuer: string;
  issuerName: string;
  rootAttributeContent: string;
  rootAssetId: string;
  schemeName: string;
  rootAttributes: UserAttribute[];
  associatedSchemes: UserAssociatedAttributes[]
}
