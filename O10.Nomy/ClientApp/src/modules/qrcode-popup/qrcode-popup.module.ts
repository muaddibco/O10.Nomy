import { NgModule } from '@angular/core';
import { QrCodeExModule } from '../qrcode/qrcode.module';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatBottomSheetModule } from '@angular/material/bottom-sheet';
import { CommonModule } from '@angular/common';
import { QrCodePopupComponent } from './qrcode-popup/qrcode-popup.component';



@NgModule({
  declarations: [
    QrCodePopupComponent
  ],
  imports: [
    CommonModule,
    BrowserModule,
    BrowserAnimationsModule,
    MatBottomSheetModule,
    QrCodeExModule
  ]
})
export class QrCodePopupModule { }
