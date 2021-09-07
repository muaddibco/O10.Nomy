"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.DuplicateAccountComponent = void 0;
var core_1 = require("@angular/core");
var forms_1 = require("@angular/forms");
var DuplicateAccountComponent = /** @class */ (function () {
    function DuplicateAccountComponent(accountAccessService, appState, router, route, formBuilder) {
        this.accountAccessService = accountAccessService;
        this.appState = appState;
        this.router = router;
        this.route = route;
        this.formBuilder = formBuilder;
        this.isLoaded = false;
        this.submitted = false;
        this.submitClick = false;
    }
    DuplicateAccountComponent.prototype.ngOnInit = function () {
        this.appState.setIsMobile(true);
        var userId = Number(this.route.snapshot.paramMap.get('userId'));
        var that = this;
        this.accountAccessService.getAccountById(userId).subscribe(function (r) {
            that.user = r;
            that.isLoaded = true;
        });
        this.duplicationForm = this.formBuilder.group({
            accountInfo: ['', forms_1.Validators.required]
        });
    };
    Object.defineProperty(DuplicateAccountComponent.prototype, "formData", {
        get: function () { return this.duplicationForm.controls; },
        enumerable: false,
        configurable: true
    });
    DuplicateAccountComponent.prototype.onSubmitDuplicating = function () {
        var _this = this;
        this.submitted = true;
        // stop here if form is invalid
        if (this.duplicationForm.invalid) {
            return;
        }
        this.submitClick = true;
        this.accountAccessService.duplicate(this.user.accountId, this.formData.accountInfo.value).subscribe(function (r) {
            _this.router.navigate(['/user-details', _this.user.accountId]);
        });
    };
    DuplicateAccountComponent.prototype.onCancelDuplicating = function () {
        this.router.navigate(['/user-details', this.user.accountId]);
    };
    DuplicateAccountComponent = __decorate([
        (0, core_1.Component)({
            selector: 'app-duplicate-account',
            templateUrl: './duplicate-account.component.html',
            styleUrls: ['./duplicate-account.component.css']
        })
    ], DuplicateAccountComponent);
    return DuplicateAccountComponent;
}());
exports.DuplicateAccountComponent = DuplicateAccountComponent;
//# sourceMappingURL=duplicate-account.component.js.map