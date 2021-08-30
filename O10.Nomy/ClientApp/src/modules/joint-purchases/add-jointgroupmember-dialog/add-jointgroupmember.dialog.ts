import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

export interface AddJointGroupMemberDialogData {
	email: string;
	description: string;
}

@Component({
  selector: 'dialog-add-jointgroupmember',
  templateUrl: 'add-jointgroupmember.dialog.html',
  styleUrls: ['add-jointgroupmember.dialog.scss']
})
export class AddJointGroupMemberDialog {
  constructor(
    public dialogRef: MatDialogRef<AddJointGroupMemberDialog>,
    @Inject(MAT_DIALOG_DATA) public data: AddJointGroupMemberDialogData) {
    this.data = { email: "", description: "" }
  }

	onCancelClick(): void {
		this.dialogRef.close();
	}
}
