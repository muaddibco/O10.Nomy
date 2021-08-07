import { ActionType } from "./action-type";
import { ValidationType } from "./validation-type";

export interface ActionDetails {
  actionType: ActionType;
  actionItemKey: string;
  accountInfo: string;
  isRegistered: boolean;
  publicKey: string;
  publicKey2: string;
  sessionKey: string;
  isBiometryRequired: boolean;
  requiredValidations: Map<string, ValidationType>;
  permittedRelations: Map<string, string[]>;
  existingRelations: string[];
}
