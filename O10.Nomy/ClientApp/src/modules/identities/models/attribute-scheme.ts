import { AssociatedScheme } from "./associated-scheme";
import { AttributeState } from "./attribute-state";
import { RootAttribute } from "./root-attribute";

export interface AttributeScheme {
  issuerAddress: string;
  issuerName: string;
  state: AttributeState;
  rootAttributeContent: string;
  rootAssetId: string;
  schemeName: string;
  rootAttributes: RootAttribute[];
  associatedSchemes: AssociatedScheme[];
}
