import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

export interface AddJointGroupDialogData {
	name: string;
	description: string;
}


@Component({
	selector: 'dialog-add-jointgroup',
  templateUrl: 'add-jointgroup.dialog.html',
  styleUrls: ['add-jointgroup.dialog.scss']
})
export class AddJointGroupDialog {
  constructor(
    public dialogRef: MatDialogRef<AddJointGroupDialog>,
    @Inject(MAT_DIALOG_DATA) public data: AddJointGroupDialogData) {
    this.data = { name: "", description: "" }
  }

	onCancelClick(): void {
		this.dialogRef.close();
	}
}
