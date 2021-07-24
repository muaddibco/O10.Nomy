import { AssociatedAttribute } from "./associated-attribute";

export interface AssociatedScheme {
  issuerAddress: string;
  issuerName: string;
  attributes: AssociatedAttribute[];
}
