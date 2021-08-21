"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.UserModule = void 0;
var core_1 = require("@angular/core");
var platform_browser_1 = require("@angular/platform-browser");
var animations_1 = require("@angular/platform-browser/animations");
var http_1 = require("@angular/common/http");
var router_1 = require("@angular/router");
var forms_1 = require("@angular/forms");
var bottom_sheet_1 = require("@angular/material/bottom-sheet");
var button_1 = require("@angular/material/button");
var button_toggle_1 = require("@angular/material/button-toggle");
var card_1 = require("@angular/material/card");
var checkbox_1 = require("@angular/material/checkbox");
var dialog_1 = require("@angular/material/dialog");
var divider_1 = require("@angular/material/divider");
var expansion_1 = require("@angular/material/expansion");
var form_field_1 = require("@angular/material/form-field");
var icon_1 = require("@angular/material/icon");
var input_1 = require("@angular/material/input");
var list_1 = require("@angular/material/list");
var progress_bar_1 = require("@angular/material/progress-bar");
var radio_1 = require("@angular/material/radio");
var select_1 = require("@angular/material/select");
var slide_toggle_1 = require("@angular/material/slide-toggle");
var stepper_1 = require("@angular/material/stepper");
var table_1 = require("@angular/material/table");
var ngx_webcam_1 = require("ngx-webcam");
var angularx_qrcode_1 = require("angularx-qrcode");
var ngx_scanner_1 = require("@zxing/ngx-scanner");
var ngx_cookie_service_1 = require("ngx-cookie-service");
var user_entry_component_1 = require("./user-entry/user-entry.component");
var accounts_module_1 = require("../accounts/accounts.module");
var experts_module_1 = require("../experts/experts.module");
var password_confirm_module_1 = require("../password-confirm/password-confirm.module");
var identities_module_1 = require("../identities/identities.module");
var user_registration_component_1 = require("./user-registration/user-registration.component");
var user_details_component_1 = require("./user-details/user-details.component");
var user_signin_component_1 = require("./user-signin/user-signin.component");
var user_attributes_component_1 = require("../identities/user-attributes/user-attributes.component");
var qr_scan_component_1 = require("./qr-scan/qr-scan.component");
var service_provider_component_1 = require("./service-provider/service-provider.component");
var UserModule = /** @class */ (function () {
    function UserModule() {
    }
    UserModule = __decorate([
        core_1.NgModule({
            declarations: [
                user_entry_component_1.UserEntryComponent,
                user_registration_component_1.UserRegistrationComponent,
                user_details_component_1.UserDetailsComponent,
                user_signin_component_1.UserSigninComponent,
                qr_scan_component_1.QrScanComponent,
                service_provider_component_1.ServiceProviderComponent
            ],
            imports: [
                platform_browser_1.BrowserModule,
                http_1.HttpClientModule,
                forms_1.FormsModule,
                forms_1.ReactiveFormsModule,
                ngx_webcam_1.WebcamModule,
                angularx_qrcode_1.QRCodeModule,
                ngx_scanner_1.ZXingScannerModule,
                animations_1.BrowserAnimationsModule,
                expansion_1.MatExpansionModule, input_1.MatInputModule, select_1.MatSelectModule, dialog_1.MatDialogModule, button_1.MatButtonModule, bottom_sheet_1.MatBottomSheetModule, card_1.MatCardModule, icon_1.MatIconModule, progress_bar_1.MatProgressBarModule, list_1.MatListModule, button_toggle_1.MatButtonToggleModule, divider_1.MatDividerModule, stepper_1.MatStepperModule, checkbox_1.MatCheckboxModule, radio_1.MatRadioModule, form_field_1.MatFormFieldModule, slide_toggle_1.MatSlideToggleModule, table_1.MatTableModule,
                accounts_module_1.AccountsModule,
                password_confirm_module_1.PasswordConfirmModule,
                experts_module_1.ExpertsModule,
                identities_module_1.IdentitiesModule,
                router_1.RouterModule.forRoot([
                    { path: 'wallet', component: user_entry_component_1.UserEntryComponent },
                    { path: 'user-entry', component: user_entry_component_1.UserEntryComponent },
                    { path: 'user-register', component: user_registration_component_1.UserRegistrationComponent },
                    { path: 'user-details/:userId', component: user_details_component_1.UserDetailsComponent },
                    { path: 'user-signin', component: user_signin_component_1.UserSigninComponent },
                    { path: 'user-attributes/:userId', component: user_attributes_component_1.UserAttributesComponent },
                    { path: 'qr-scan/:userId', component: qr_scan_component_1.QrScanComponent },
                    { path: 'service-provider/:userId', component: service_provider_component_1.ServiceProviderComponent }
                ]),
            ],
            providers: [ngx_cookie_service_1.CookieService]
        })
    ], UserModule);
    return UserModule;
}());
exports.UserModule = UserModule;
//# sourceMappingURL=user.module.js.map