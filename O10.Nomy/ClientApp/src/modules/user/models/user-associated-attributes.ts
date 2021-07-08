import { UserAssociatedAttribute } from "./user-associated-attribute";

export interface UserAssociatedAttributes {
  issuer: string;
  issuerName: string;
  attributes: UserAssociatedAttribute[];
}
