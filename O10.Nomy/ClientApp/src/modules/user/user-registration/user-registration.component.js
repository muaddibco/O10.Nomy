"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.UserRegistrationComponent = void 0;
var core_1 = require("@angular/core");
var forms_1 = require("@angular/forms");
var UserRegistrationComponent = /** @class */ (function () {
    function UserRegistrationComponent(formBuilder, userAccessService, router) {
        this.formBuilder = formBuilder;
        this.userAccessService = userAccessService;
        this.router = router;
        this.isLoaded = false;
        this.submitted = false;
        this.submitClick = false;
    }
    UserRegistrationComponent.prototype.ngOnInit = function () {
        this.newUserForm = this.formBuilder.group({
            firstName: ['', forms_1.Validators.required],
            lastName: ['', forms_1.Validators.required],
            email: ['', forms_1.Validators.required, forms_1.Validators.email],
            password: ['', forms_1.Validators.required]
        });
        this.isLoaded = true;
    };
    Object.defineProperty(UserRegistrationComponent.prototype, "formData", {
        get: function () { return this.newUserForm.controls; },
        enumerable: false,
        configurable: true
    });
    UserRegistrationComponent.prototype.onSubmit = function () {
        var _this = this;
        this.submitted = true;
        // stop here if form is invalid
        if (this.newUserForm.invalid) {
            return;
        }
        this.submitClick = true;
        this.userAccessService.register({
            firstName: this.formData.firstName.value,
            lastName: this.formData.lastName.value,
            email: this.formData.email.value,
            password: this.formData.password.value
        }).subscribe(function (a) {
            _this.router.navigate(['user-details', a.accountId]);
        }, function (e) {
        });
    };
    UserRegistrationComponent.prototype.onCancel = function () {
        this.router.navigate(['/']);
    };
    UserRegistrationComponent = __decorate([
        core_1.Component({
            selector: 'app-user-registration',
            templateUrl: './user-registration.component.html',
            styleUrls: ['./user-registration.component.css']
        })
    ], UserRegistrationComponent);
    return UserRegistrationComponent;
}());
exports.UserRegistrationComponent = UserRegistrationComponent;
//# sourceMappingURL=user-registration.component.js.map