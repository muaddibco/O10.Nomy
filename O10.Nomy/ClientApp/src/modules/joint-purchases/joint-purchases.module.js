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
var qrcode_module_1 = require("../qrcode/qrcode.module");
var joint_entry_component_1 = require("./joint-entry/joint-entry.component");
var JointPurchasesModule = /** @class */ (function () {
    function JointPurchasesModule() {
    }
    JointPurchasesModule = __decorate([
        core_1.NgModule({
            declarations: [
                joint_entry_component_1.JointEntryComponent
            ],
            imports: [
                platform_browser_1.BrowserModule,
                animations_1.BrowserAnimationsModule,
                http_1.HttpClientModule,
                forms_1.FormsModule,
                forms_1.ReactiveFormsModule,
                common_1.CommonModule,
                qrcode_module_1.QrCodeExModule,
                router_1.RouterModule.forRoot([
                    { path: 'joint-purchases', component: joint_entry_component_1.JointEntryComponent }
                ])
            ]
        })
    ], JointPurchasesModule);
    return JointPurchasesModule;
}());
exports.JointPurchasesModule = JointPurchasesModule;
//# sourceMappingURL=joint-purchases.module.js.map