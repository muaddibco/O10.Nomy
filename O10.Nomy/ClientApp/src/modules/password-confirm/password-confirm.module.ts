import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
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

import { PasswordConfirmDialog } from './password-confirm/password-confirm.dialog';


@NgModule({
  declarations: [
    PasswordConfirmDialog
  ],
  imports: [
    CommonModule,
    BrowserModule,
    FormsModule, ReactiveFormsModule,
    BrowserAnimationsModule,
    MatExpansionModule, MatInputModule, MatSelectModule, MatDialogModule, MatButtonModule, MatBottomSheetModule, MatCardModule, MatIconModule, MatProgressBarModule, MatListModule, MatButtonToggleModule, MatDividerModule, MatStepperModule, MatCheckboxModule, MatRadioModule, MatFormFieldModule, MatSlideToggleModule,
  ],
  exports: [
    PasswordConfirmDialog
  ],
  bootstrap: [
    PasswordConfirmDialog
  ]
})
export class PasswordConfirmModule { }
