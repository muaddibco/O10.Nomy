"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.RevealSecretsComponent = void 0;
var core_1 = require("@angular/core");
var password_confirm_dialog_1 = require("../../password-confirm/password-confirm/password-confirm.dialog");
var RevealSecretsComponent = /** @class */ (function () {
    function RevealSecretsComponent(router, route, dialog, userAccessService, appState) {
        this.router = router;
        this.route = route;
        this.dialog = dialog;
        this.userAccessService = userAccessService;
        this.appState = appState;
        this.isLoaded = false;
        this.showQr = false;
    }
    RevealSecretsComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.appState.setIsMobile(true);
        this.userId = Number(this.route.snapshot.paramMap.get('userId'));
        var that = this;
        var dialogRef = this.dialog.open(password_confirm_dialog_1.PasswordConfirmDialog, { data: { confirmButtonText: "Confirm Disclose" } });
        dialogRef.afterClosed().subscribe(function (r) {
            if (r) {
                that.userAccessService.getDisclosedSecrets(_this.userId, r).subscribe(function (r1) {
                    that.qrCode = r1.code;
                    that.showQr = true;
                    that.isLoaded = true;
                }, function (e) {
                    that.showQr = false;
                    that.isLoaded = true;
                    alert('Failed to disclose secrets');
                });
            }
            else {
                that.showQr = false;
                that.isLoaded = true;
            }
        });
    };
    RevealSecretsComponent.prototype.cancel = function () {
        this.router.navigate(['user-details', this.userId]);
    };
    RevealSecretsComponent = __decorate([
        (0, core_1.Component)({
            selector: 'app-reveal-secrets',
            templateUrl: './reveal-secrets.component.html',
            styleUrls: ['./reveal-secrets.component.css']
        })
    ], RevealSecretsComponent);
    return RevealSecretsComponent;
}());
exports.RevealSecretsComponent = RevealSecretsComponent;
//# sourceMappingURL=reveal-secrets.component.js.map