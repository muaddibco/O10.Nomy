import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

import { QrCodeExModule } from '../qrcode/qrcode.module'

import { JointEntryComponent } from './joint-entry/joint-entry.component';



@NgModule({
  declarations: [
    JointEntryComponent
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
      { path: 'joint-purchases', component: JointEntryComponent }
    ])
  ]
})
export class JointPurchasesModule { }
