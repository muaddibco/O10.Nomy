import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

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

import { WebcamModule } from 'ngx-webcam';
import { QRCodeModule } from 'angularx-qrcode';
import { ZXingScannerModule } from '@zxing/ngx-scanner';
import { CookieService } from 'ngx-cookie-service';
import { UserEntryComponent } from './user-entry/user-entry.component';
import { AccountsModule } from '../accounts/accounts.module';
import { ExpertsModule } from '../experts/experts.module'
import { PasswordConfirmModule } from '../password-confirm/password-confirm.module'

import { UserRegistrationComponent } from './user-registration/user-registration.component';
import { UserDetailsComponent } from './user-details/user-details.component';
import { UserSigninComponent } from './user-signin/user-signin.component';

@NgModule({
  declarations: [
    UserEntryComponent,
    UserRegistrationComponent,
    UserDetailsComponent,
    UserSigninComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    WebcamModule,
    QRCodeModule,
    ZXingScannerModule,
    BrowserAnimationsModule,
    MatExpansionModule, MatInputModule, MatSelectModule, MatDialogModule, MatButtonModule, MatBottomSheetModule, MatCardModule, MatIconModule, MatProgressBarModule, MatListModule, MatButtonToggleModule, MatDividerModule, MatStepperModule, MatCheckboxModule, MatRadioModule, MatFormFieldModule, MatSlideToggleModule,
    AccountsModule,
    PasswordConfirmModule,
    ExpertsModule,
    RouterModule.forRoot([
      { path: 'user-entry', component: UserEntryComponent },
      { path: 'user-register', component: UserRegistrationComponent },
      { path: 'user-details/:userId', component: UserDetailsComponent },
      { path: 'user-signin', component: UserSigninComponent }
    ]),
  ],
  providers: [CookieService]
})
export class UserModule { }
