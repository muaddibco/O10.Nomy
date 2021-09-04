"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.QrScanComponent = void 0;
var core_1 = require("@angular/core");
var user_action_type_1 = require("../models/user-action-type");
var QrScanComponent = /** @class */ (function () {
    function QrScanComponent(appState, service, route, router) {
        this.appState = appState;
        this.service = service;
        this.route = route;
        this.router = router;
        this.isLoaded = false;
        this.isQrCodeSet = false;
    }
    QrScanComponent.prototype.ngOnInit = function () {
        this.appState.setIsMobile(true);
        this.userId = Number(this.route.snapshot.paramMap.get('userId'));
        this.isLoaded = true;
    };
    QrScanComponent.prototype.getDataFromClipBoard = function (event) {
        var _this = this;
        navigator['clipboard'].readText().then(function (data) {
            _this.qrContent = data;
            _this.isQrCodeSet = true;
        });
    };
    QrScanComponent.prototype.clearQr = function () {
        this.isQrCodeSet = false;
        this.qrContent = null;
    };
    QrScanComponent.prototype.proceed = function () {
        var _this = this;
        this.service.getActionInfo(this.qrContent).subscribe(function (r) {
            if (r.actionType == user_action_type_1.UserActionType.ServiceProvider) {
                _this.router.navigate(['service-provider', _this.userId], { queryParams: { actionInfo: r.actionInfoEncoded } });
            }
        }, function (e) {
            console.error("failed to obtain action info", e);
        });
    };
    QrScanComponent.prototype.cancel = function () {
        this.router.navigate(['user-details', this.userId]);
    };
    QrScanComponent = __decorate([
        (0, core_1.Component)({
            selector: 'app-qr-scan',
            templateUrl: './qr-scan.component.html',
            styleUrls: ['./qr-scan.component.css']
        })
    ], QrScanComponent);
    return QrScanComponent;
}());
exports.QrScanComponent = QrScanComponent;
//# sourceMappingURL=qr-scan.component.js.map