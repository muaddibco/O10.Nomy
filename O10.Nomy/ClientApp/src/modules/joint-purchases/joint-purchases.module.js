"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.JointPurchasesModule = void 0;
var core_1 = require("@angular/core");
var platform_browser_1 = require("@angular/platform-browser");
var animations_1 = require("@angular/platform-browser/animations");
var http_1 = require("@angular/common/http");
var router_1 = require("@angular/router");
var forms_1 = require("@angular/forms");
var common_1 = require("@angular/common");
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
var qrcode_module_1 = require("../qrcode/qrcode.module");
var joint_entry_component_1 = require("./joint-entry/joint-entry.component");
var joint_main_component_1 = require("./joint-main/joint-main.component");
var add_jointgroup_dialog_1 = require("./add-jointgroup-dialog/add-jointgroup.dialog");
var joint_group_admin_component_1 = require("./joint-group-admin/joint-group-admin.component");
var JointPurchasesModule = /** @class */ (function () {
    function JointPurchasesModule() {
    }
    JointPurchasesModule = __decorate([
        core_1.NgModule({
            declarations: [
                joint_entry_component_1.JointEntryComponent,
                joint_main_component_1.JointMainComponent,
                add_jointgroup_dialog_1.AddJointGroupDialog,
                joint_group_admin_component_1.JointGroupAdminComponent
            ],
            imports: [
                platform_browser_1.BrowserModule,
                animations_1.BrowserAnimationsModule,
                http_1.HttpClientModule,
                forms_1.FormsModule,
                forms_1.ReactiveFormsModule,
                common_1.CommonModule,
                expansion_1.MatExpansionModule, input_1.MatInputModule, select_1.MatSelectModule, dialog_1.MatDialogModule, button_1.MatButtonModule, bottom_sheet_1.MatBottomSheetModule, card_1.MatCardModule, icon_1.MatIconModule, progress_bar_1.MatProgressBarModule, list_1.MatListModule, button_toggle_1.MatButtonToggleModule, divider_1.MatDividerModule, stepper_1.MatStepperModule, checkbox_1.MatCheckboxModule, radio_1.MatRadioModule, form_field_1.MatFormFieldModule, slide_toggle_1.MatSlideToggleModule, table_1.MatTableModule,
                qrcode_module_1.QrCodeExModule,
                router_1.RouterModule.forRoot([
                    { path: 'joint-purchases', component: joint_entry_component_1.JointEntryComponent },
                    { path: 'joint-main/:registrationId/:sessionKey', component: joint_main_component_1.JointMainComponent },
                    { path: 'joint-group-admin/:groupId/:registrationId/:sessionKey', component: joint_group_admin_component_1.JointGroupAdminComponent }
                ])
            ]
        })
    ], JointPurchasesModule);
    return JointPurchasesModule;
}());
exports.JointPurchasesModule = JointPurchasesModule;
//# sourceMappingURL=joint-purchases.module.js.map