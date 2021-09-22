"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.IdentitiesModule = void 0;
var core_1 = require("@angular/core");
var common_1 = require("@angular/common");
var platform_browser_1 = require("@angular/platform-browser");
var animations_1 = require("@angular/platform-browser/animations");
var http_1 = require("@angular/common/http");
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
var password_confirm_module_1 = require("../password-confirm/password-confirm.module");
var user_attributes_component_1 = require("./user-attributes/user-attributes.component");
var IdentitiesModule = /** @class */ (function () {
    function IdentitiesModule() {
    }
    IdentitiesModule = __decorate([
        (0, core_1.NgModule)({
            declarations: [
                user_attributes_component_1.UserAttributesComponent
            ],
            imports: [
                common_1.CommonModule,
                platform_browser_1.BrowserModule,
                http_1.HttpClientModule,
                animations_1.BrowserAnimationsModule,
                expansion_1.MatExpansionModule, input_1.MatInputModule, select_1.MatSelectModule, dialog_1.MatDialogModule, button_1.MatButtonModule, bottom_sheet_1.MatBottomSheetModule, card_1.MatCardModule, icon_1.MatIconModule, progress_bar_1.MatProgressBarModule, list_1.MatListModule, button_toggle_1.MatButtonToggleModule, divider_1.MatDividerModule, stepper_1.MatStepperModule, checkbox_1.MatCheckboxModule, radio_1.MatRadioModule, form_field_1.MatFormFieldModule, slide_toggle_1.MatSlideToggleModule, table_1.MatTableModule,
                password_confirm_module_1.PasswordConfirmModule
            ]
        })
    ], IdentitiesModule);
    return IdentitiesModule;
}());
exports.IdentitiesModule = IdentitiesModule;
//# sourceMappingURL=identities.module.js.map