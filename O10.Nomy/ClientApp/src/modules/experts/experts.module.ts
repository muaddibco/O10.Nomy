import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

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

import { ExpertChatComponent } from './expert-chat/expert-chat.component';
import { ExpertsListComponent } from './experts-list/experts-list.component';


@NgModule({
  declarations: [
    ExpertsListComponent,
    ExpertChatComponent
  ],
  imports: [
    CommonModule, BrowserModule, BrowserAnimationsModule, HttpClientModule,
    MatExpansionModule, MatInputModule, MatSelectModule, MatDialogModule, MatButtonModule, MatBottomSheetModule, MatCardModule, MatIconModule, MatProgressBarModule, MatListModule, MatButtonToggleModule, MatDividerModule, MatStepperModule, MatCheckboxModule, MatRadioModule, MatFormFieldModule, MatSlideToggleModule, MatTableModule,
    RouterModule.forRoot([
      { path: 'expert-chat/:userId/:id', component: ExpertChatComponent},
      { path: 'experts-list/:userId', component: ExpertsListComponent }
    ])
  ]
})
export class ExpertsModule { }
