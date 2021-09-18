"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.CompromizedComponent = void 0;
var core_1 = require("@angular/core");
var password_confirm_dialog_1 = require("../../password-confirm/password-confirm/password-confirm.dialog");
var CompromizedComponent = /** @class */ (function () {
    function CompromizedComponent(appState, router, route, dialog, accountsAccessService) {
        this.appState = appState;
        this.router = router;
        this.route = route;
        this.dialog = dialog;
        this.accountsAccessService = accountsAccessService;
        this.isLoaded = false;
    }
    CompromizedComponent.prototype.ngOnInit = function () {
        this.appState.setIsMobile(true);
        this.userId = Number(this.route.snapshot.paramMap.get('userId'));
        this.isLoaded = true;
    };
    CompromizedComponent.prototype.onReset = function () {
        var _this = this;
        var that = this;
        var dialogRef = this.dialog.open(password_confirm_dialog_1.PasswordConfirmDialog, { data: { title: "Confirm account reset", confirmButtonText: "Confirm Reset" } });
        dialogRef.afterClosed().subscribe(function (r) {
            if (r) {
                that.accountsAccessService.reset(_this.userId, r).subscribe(function (r1) {
                    console.info('Reset of account passed successfully');
                    _this.router.navigate(['user-details', _this.userId]);
                }, function (e) {
                    console.error('Failed to reset compromised account', e);
                });
            }
        });
    };
    CompromizedComponent = __decorate([
        (0, core_1.Component)({
            selector: 'app-compromized',
            templateUrl: './compromized.component.html',
            styleUrls: ['./compromized.component.css']
        })
    ], CompromizedComponent);
    return CompromizedComponent;
}());
exports.CompromizedComponent = CompromizedComponent;
//# sourceMappingURL=compromized.component.js.map