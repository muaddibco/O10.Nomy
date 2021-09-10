"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.UserSigninComponent = void 0;
var core_1 = require("@angular/core");
var UserSigninComponent = /** @class */ (function () {
    function UserSigninComponent(formBuilder, router, appState, userAccessService) {
        this.formBuilder = formBuilder;
        this.router = router;
        this.appState = appState;
        this.userAccessService = userAccessService;
        this.showError = false;
        this.isLoaded = false;
        this.submitted = false;
        this.submitClick = false;
    }
    UserSigninComponent.prototype.ngOnInit = function () {
        this.appState.setIsMobile(true);
        this.accountNameForm = this.formBuilder.group({
            accountEmail: ['']
        });
        this.isLoaded = true;
    };
    Object.defineProperty(UserSigninComponent.prototype, "formData", {
        get: function () { return this.accountNameForm.controls; },
        enumerable: false,
        configurable: true
    });
    UserSigninComponent.prototype.onSignIn = function () {
        var _this = this;
        this.submitted = true;
        // stop here if form is invalid
        if (this.accountNameForm.invalid) {
            return;
        }
        this.submitClick = true;
        this.showError = false;
        this.userAccessService.find(this.formData.accountEmail.value).subscribe(function (a) {
            if (a == null) {
                _this.showError = true;
            }
            else {
                _this.router.navigate(['/user-details', a.accountId]);
            }
        });
    };
    UserSigninComponent.prototype.onCancel = function () {
        this.router.navigate(['/user-entry']);
    };
    UserSigninComponent = __decorate([
        (0, core_1.Component)({
            selector: 'app-user-signin',
            templateUrl: './user-signin.component.html',
            styleUrls: ['./user-signin.component.css']
        })
    ], UserSigninComponent);
    return UserSigninComponent;
}());
exports.UserSigninComponent = UserSigninComponent;
//# sourceMappingURL=user-signin.component.js.map