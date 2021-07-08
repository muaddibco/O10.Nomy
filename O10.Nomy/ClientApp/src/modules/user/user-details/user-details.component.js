"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.UserDetailsComponent = void 0;
var core_1 = require("@angular/core");
var password_confirm_dialog_1 = require("../../password-confirm/password-confirm/password-confirm.dialog");
var signalr_1 = require("@microsoft/signalr");
var rxjs_1 = require("rxjs");
var UserDetailsComponent = /** @class */ (function () {
    function UserDetailsComponent(userAccessService, expertAccessService, router, dialog) {
        this.userAccessService = userAccessService;
        this.expertAccessService = expertAccessService;
        this.router = router;
        this.dialog = dialog;
        this.isLoaded = false;
        this.isInSession = false;
        this.isSessionStarted = false;
    }
    UserDetailsComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.user = JSON.parse(sessionStorage.getItem("user"));
        var that = this;
        var dialogRef = this.dialog.open(password_confirm_dialog_1.PasswordConfirmDialog, { data: { title: "Start account", confirmButtonText: "Submit" } });
        dialogRef.afterClosed().subscribe(function (r) {
            if (r) {
            }
            that.isLoaded = true;
        });
        this.userAccessService.getUserAttributes(this.user).subscribe(function (r) {
            if (r && r.length > 0) {
                _this.nomyIdentity = r[0];
            }
        }, function (e) {
        });
        this.paymentHub = new signalr_1.HubConnectionBuilder()
            .withUrl('/payments')
            .withAutomaticReconnect()
            .build();
        this.paymentHub.start();
        this.chatHub = new signalr_1.HubConnectionBuilder()
            .withUrl('/chat')
            .build();
        this.initiateChatHub(this);
        this.chatHub.on("Invitation", function (r) {
            _this.sessionInfo = r;
            if (confirm("There is an invitation for a session with id " + _this.sessionInfo.sessionId)) {
                _this.isInSession = true;
                _this.expertAccessService.getExpert(_this.sessionInfo.expertProfileId).subscribe(function (r) {
                    _this.expertProfile = r;
                });
                _this.paymentHub.invoke("AddToGroup", _this.sessionInfo.sessionId + "_Payee");
                _this.userAccessService.confirmSession(_this.sessionInfo.sessionId).subscribe(function (r) {
                }, function (e) {
                });
            }
        });
        this.chatHub.on("Start", function (r) {
            _this.isSessionStarted = true;
            _this.paymentSubscription = rxjs_1.interval(30000).subscribe(function (v) {
            });
        });
    };
    UserDetailsComponent.prototype.initiateChatHub = function (that) {
        var _this = this;
        this.chatHub.start().then(function () {
            console.info("Connected to chatHub");
            _this.chatHub.invoke("AddToGroup", that.user.accountId.toString()).then(function () {
                console.info("Added to chatHub group " + that.user.accountId.toString());
            }).catch(function (e) {
                console.error(e);
            });
        }).catch(function (e) {
            console.error(e);
            setTimeout(function () { return that.initiateChatHub(that); }, 1000);
        });
    };
    UserDetailsComponent.prototype.ngOnDestroy = function () {
        if (this.paymentSubscription) {
            this.paymentSubscription.unsubscribe();
        }
    };
    UserDetailsComponent.prototype.gotoExperts = function () {
        this.router.navigate(['experts-list', this.user.accountId]);
    };
    UserDetailsComponent = __decorate([
        core_1.Component({
            selector: 'app-user-details',
            templateUrl: './user-details.component.html',
            styleUrls: ['./user-details.component.css'],
            encapsulation: core_1.ViewEncapsulation.None
        })
    ], UserDetailsComponent);
    return UserDetailsComponent;
}());
exports.UserDetailsComponent = UserDetailsComponent;
//# sourceMappingURL=user-details.component.js.map