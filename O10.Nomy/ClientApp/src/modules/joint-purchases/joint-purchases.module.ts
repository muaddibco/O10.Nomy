import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

import { QrCodeExModule } from '../qrcode/qrcode.module'

import { JointEntryComponent } from './joint-entry/joint-entry.component';
import { JointMainComponent } from './joint-main/joint-main.component';



@NgModule({
  declarations: [
    JointEntryComponent,
    JointMainComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    QrCodeExModule,
    RouterModule.forRoot([
      { path: 'joint-purchases', component: JointEntryComponent },
      { path: 'joint-main/:sessionKey', component: JointMainComponent }
    ])
  ]
})
export class JointPurchasesModule { }
