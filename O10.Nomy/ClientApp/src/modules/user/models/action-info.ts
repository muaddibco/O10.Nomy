import { UserActionType } from "./user-action-type";

export interface ActionInfo {
  actionType: UserActionType;
  actionInfoEncoded: string;
}
