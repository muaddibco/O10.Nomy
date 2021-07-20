"use strict";
var __extends = (this && this.__extends) || (function () {
    var extendStatics = function (d, b) {
        extendStatics = Object.setPrototypeOf ||
            ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
            function (d, b) { for (var p in b) if (Object.prototype.hasOwnProperty.call(b, p)) d[p] = b[p]; };
        return extendStatics(d, b);
    };
    return function (d, b) {
        if (typeof b !== "function" && b !== null)
            throw new TypeError("Class extends value " + String(b) + " is not a constructor or null");
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
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
var collections_1 = require("@angular/cdk/collections");
var rxjs_1 = require("rxjs");
var animations_1 = require("@angular/animations");
var rxjs_2 = require("rxjs");
var UserDetailsComponent = /** @class */ (function () {
    function UserDetailsComponent(userAccessService, accountAccessService, expertAccessService, router, route, dialog) {
        this.userAccessService = userAccessService;
        this.accountAccessService = accountAccessService;
        this.expertAccessService = expertAccessService;
        this.router = router;
        this.route = route;
        this.dialog = dialog;
        this.isLoaded = false;
        this.isInSession = false;
        this.isSessionStarted = false;
        this.payments = [];
        this.dataSource = new PaymentsDataSource(this.payments);
        this.displayedColumns = ['commitment'];
    }
    UserDetailsComponent.prototype.ngOnInit = function () {
        var _this = this;
        var userId = Number(this.route.snapshot.paramMap.get('userId'));
        var that = this;
        this.accountAccessService.getAccountById(userId).subscribe(function (r) {
            that.user = r;
            var dialogRef = that.dialog.open(password_confirm_dialog_1.PasswordConfirmDialog, { data: { title: "Start account", confirmButtonText: "Submit" } });
            dialogRef.afterClosed().subscribe(function (r) {
                if (r) {
                    that.initiateChatHub(that);
                    that.initiateUserAttributes(that, r);
                }
                that.isLoaded = true;
            });
        }, function (e) {
        });
        this.paymentHub = new signalr_1.HubConnectionBuilder()
            .withUrl('/payments')
            .withAutomaticReconnect()
            .build();
        this.paymentHub.start();
        this.paymentHub.on("Payment", function (p) {
            var paymentEntry = p;
            console.info("Obtained payment " + paymentEntry.commitment);
            that.payments.push(paymentEntry);
            that.dataSource.setData(that.payments);
        });
        this.chatHub = new signalr_1.HubConnectionBuilder()
            .withUrl('/chat')
            .build();
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
            var sessionInfo = r;
            console.info("Started session " + sessionInfo.sessionId + ", launching periodic invoice issuing...");
            _this.isSessionStarted = true;
            _this.issueInvoice();
            _this.paymentSubscription = rxjs_1.interval(60000).subscribe(function (v) {
                _this.issueInvoice();
            });
        });
    };
    UserDetailsComponent.prototype.issueInvoice = function () {
        var _this = this;
        console.info("Issue invoice for " + this.expertProfile.fee + " USD");
        this.userAccessService.sendInvoice(this.user.accountId, this.sessionInfo.sessionId, this.expertProfile.fee, "USD").subscribe(function (r) {
            console.info("Invoice " + r.commitment + " for the session " + _this.sessionInfo.sessionId + " issued successfully");
        }, function (e) {
            console.error("Failed to issue an invoice for the session " + _this.sessionInfo.sessionId, e);
        });
    };
    UserDetailsComponent.prototype.initiateUserAttributes = function (that, password) {
        that.userAccessService.start(that.user.accountId, password).subscribe(function (a) {
            that.userAccessService.getUserAttributes(that.user.accountId).subscribe(function (r) {
                if (r && r.length > 0) {
                    console.log("There are " + r.length + " user attributes");
                    that.nomyIdentity = r[0];
                    console.log(that.nomyIdentity);
                }
                else {
                    console.warn("No user attributes obtained");
                }
            }, function (e) {
                console.error("Failed to obtain user attributes", e);
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
            encapsulation: core_1.ViewEncapsulation.None,
            animations: [
                animations_1.trigger('detailExpand', [
                    animations_1.state('collapsed', animations_1.style({ height: '0px', minHeight: '0' })),
                    animations_1.state('expanded', animations_1.style({ height: '*' })),
                    animations_1.transition('expanded <=> collapsed', animations_1.animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
                ]),
            ]
        })
    ], UserDetailsComponent);
    return UserDetailsComponent;
}());
exports.UserDetailsComponent = UserDetailsComponent;
var PaymentsDataSource = /** @class */ (function (_super) {
    __extends(PaymentsDataSource, _super);
    function PaymentsDataSource(initialData) {
        var _this = _super.call(this) || this;
        _this._dataStream = new rxjs_2.ReplaySubject();
        _this.setData(initialData);
        return _this;
    }
    PaymentsDataSource.prototype.connect = function () {
        return this._dataStream;
    };
    PaymentsDataSource.prototype.disconnect = function () { };
    PaymentsDataSource.prototype.setData = function (data) {
        this._dataStream.next(data);
    };
    return PaymentsDataSource;
}(collections_1.DataSource));
//# sourceMappingURL=user-details.component.js.map