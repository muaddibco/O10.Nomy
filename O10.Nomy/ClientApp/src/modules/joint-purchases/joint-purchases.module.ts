import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

import { MatBottomSheetModule } from '@angular/material/bottom-sheet';
import { MatButtonModule } from '@angular/material/button';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatCardModule } from '@angular/material/card';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatDialogModule } from '@angular/material/dialog';
import { MatDividerModule } from '@angular/material/divider';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatListModule } from '@angular/material/list';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatRadioModule } from '@angular/material/radio';
import { MatSelectModule } from '@angular/material/select';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatStepperModule } from '@angular/material/stepper';
import { MatTableModule } from '@angular/material/table';

import { QrCodeExModule } from '../qrcode/qrcode.module'

import { JointEntryComponent } from './joint-entry/joint-entry.component';
import { JointMainComponent } from './joint-main/joint-main.component';
import { AddJointGroupDialog } from './add-jointgroup-dialog/add-jointgroup.dialog';
import { JointGroupAdminComponent } from './joint-group-admin/joint-group-admin.component';
import { JointGroupComponent } from './joint-group/joint-group.component'


@NgModule({
  declarations: [
    JointEntryComponent,
    JointMainComponent,
    AddJointGroupDialog,
    JointGroupAdminComponent,
    JointGroupComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    MatExpansionModule, MatInputModule, MatSelectModule, MatDialogModule, MatButtonModule, MatBottomSheetModule, MatCardModule, MatIconModule, MatProgressBarModule, MatListModule, MatButtonToggleModule, MatDividerModule, MatStepperModule, MatCheckboxModule, MatRadioModule, MatFormFieldModule, MatSlideToggleModule, MatTableModule,
    QrCodeExModule,
    RouterModule.forRoot([
      { path: 'joint-purchases', component: JointEntryComponent },
      { path: 'joint-main/:registrationId/:sessionKey', component: JointMainComponent },
      { path: 'joint-group-admin/:groupId/:registrationId/:sessionKey', component: JointGroupAdminComponent }
    ])
  ]
})
export class JointPurchasesModule { }
