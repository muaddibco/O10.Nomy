"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.QrCodePopupModule = void 0;
var core_1 = require("@angular/core");
var qrcode_module_1 = require("../qrcode/qrcode.module");
var platform_browser_1 = require("@angular/platform-browser");
var animations_1 = require("@angular/platform-browser/animations");
var bottom_sheet_1 = require("@angular/material/bottom-sheet");
var common_1 = require("@angular/common");
var qrcode_popup_component_1 = require("./qrcode-popup/qrcode-popup.component");
var QrCodePopupModule = /** @class */ (function () {
    function QrCodePopupModule() {
    }
    QrCodePopupModule = __decorate([
        (0, core_1.NgModule)({
            declarations: [
                qrcode_popup_component_1.QrCodePopupComponent
            ],
            imports: [
                common_1.CommonModule,
                platform_browser_1.BrowserModule,
                animations_1.BrowserAnimationsModule,
                bottom_sheet_1.MatBottomSheetModule,
                qrcode_module_1.QrCodeExModule
            ]
        })
    ], QrCodePopupModule);
    return QrCodePopupModule;
}());
exports.QrCodePopupModule = QrCodePopupModule;
//# sourceMappingURL=qrcode-popup.module.js.map