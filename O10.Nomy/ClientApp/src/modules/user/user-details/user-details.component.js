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
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
var __generator = (this && this.__generator) || function (thisArg, body) {
    var _ = { label: 0, sent: function() { if (t[0] & 1) throw t[1]; return t[1]; }, trys: [], ops: [] }, f, y, t, g;
    return g = { next: verb(0), "throw": verb(1), "return": verb(2) }, typeof Symbol === "function" && (g[Symbol.iterator] = function() { return this; }), g;
    function verb(n) { return function (v) { return step([n, v]); }; }
    function step(op) {
        if (f) throw new TypeError("Generator is already executing.");
        while (_) try {
            if (f = 1, y && (t = op[0] & 2 ? y["return"] : op[0] ? y["throw"] || ((t = y["return"]) && t.call(y), 0) : y.next) && !(t = t.call(y, op[1])).done) return t;
            if (y = 0, t) op = [op[0] & 2, t.value];
            switch (op[0]) {
                case 0: case 1: t = op; break;
                case 4: _.label++; return { value: op[1], done: false };
                case 5: _.label++; y = op[1]; op = [0]; continue;
                case 7: op = _.ops.pop(); _.trys.pop(); continue;
                default:
                    if (!(t = _.trys, t = t.length > 0 && t[t.length - 1]) && (op[0] === 6 || op[0] === 2)) { _ = 0; continue; }
                    if (op[0] === 3 && (!t || (op[1] > t[0] && op[1] < t[3]))) { _.label = op[1]; break; }
                    if (op[0] === 6 && _.label < t[1]) { _.label = t[1]; t = op; break; }
                    if (t && _.label < t[2]) { _.label = t[2]; _.ops.push(op); break; }
                    if (t[2]) _.ops.pop();
                    _.trys.pop(); continue;
            }
            op = body.call(thisArg, _);
        } catch (e) { op = [6, e]; y = 0; } finally { f = t = 0; }
        if (op[0] & 5) throw op[1]; return { value: op[0] ? op[1] : void 0, done: true };
    }
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
    function UserDetailsComponent(userAccessService, accountAccessService, expertAccessService, appState, router, route, dialog) {
        this.userAccessService = userAccessService;
        this.accountAccessService = accountAccessService;
        this.expertAccessService = expertAccessService;
        this.appState = appState;
        this.router = router;
        this.route = route;
        this.dialog = dialog;
        this.isLoaded = false;
        this.isInSession = false;
        this.isSessionStarted = false;
        this.payments = [];
        this.dataSource = new PaymentsDataSource(this.payments);
        this.displayedColumns = ['commitment'];
        this.qrCodeSheetRef = null;
    }
    UserDetailsComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.appState.setIsMobile(true);
        var userId = Number(this.route.snapshot.paramMap.get('userId'));
        var that = this;
        this.accountAccessService.getAccountById(userId).subscribe(function (r) { return __awaiter(_this, void 0, void 0, function () {
            var passwordSet, res, dialogRef;
            var _this = this;
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        that.user = r;
                        passwordSet = false;
                        if (!(sessionStorage.getItem("passwordSet") === "true")) return [3 /*break*/, 2];
                        return [4 /*yield*/, that.accountAccessService.isAuthenticated(r.accountId).toPromise()];
                    case 1:
                        res = _a.sent();
                        passwordSet = res.isAuthenticated;
                        _a.label = 2;
                    case 2:
                        if (!passwordSet) {
                            dialogRef = that.dialog.open(password_confirm_dialog_1.PasswordConfirmDialog, { data: { title: "Start account", confirmButtonText: "Submit" } });
                            dialogRef.afterClosed().subscribe(function (r) {
                                if (r) {
                                    that.authenticateUser(that, r);
                                }
                                that.userAccessService.getUserAccountDetails(that.user.accountId).subscribe(function (r) {
                                    if (r.isCompromised) {
                                        that.router.navigate(['/compromized', that.user.accountId]);
                                    }
                                }, function (e) { });
                                that.isLoaded = true;
                            });
                        }
                        else {
                            that.isLoaded = true;
                            that.userAccessService.getUserAccountDetails(that.user.accountId).subscribe(function (r) {
                                if (r.isCompromised) {
                                    that.router.navigate(['/compromized', that.user.accountId]);
                                }
                            }, function (e) { });
                            that.initiateChatHub(that);
                        }
                        that.userAccessService.getO10HubUri().subscribe(function (r) {
                            console.info("Connecting to O10 Hub with URI " + r["o10HubUri"]);
                            that.o10Hub = new signalr_1.HubConnectionBuilder()
                                .withAutomaticReconnect()
                                .configureLogging(signalr_1.LogLevel.Debug)
                                .withUrl(r["o10HubUri"])
                                .build();
                            that.o10Hub.onreconnected(function (c) {
                                _this.o10Hub.invoke("AddToGroup", that.user.o10Id.toString()).then(function () {
                                    console.info("Added to o10Hub group " + that.user.o10Id.toString() + " for connection " + that.o10Hub.connectionId);
                                }).catch(function (e) {
                                    console.error(e);
                                });
                            });
                            that.o10Hub.on("PushUnauthorizedUse", function (r) {
                                console.info("Handled PushUnauthorizedUse: " + JSON.stringify(r));
                                that.userAccessService.sendCompromizationClaim(that.user.accountId, r).subscribe(function (r) {
                                    that.router.navigate(['compromized', that.user.accountId]);
                                }, function (e) {
                                    console.error("Failed to send compromization claim", e);
                                });
                            });
                            that.initiateO10Hub(that);
                        });
                        return [2 /*return*/];
                }
            });
        }); }, function (e) {
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
            _this.paymentSubscription = (0, rxjs_1.interval)(60000).subscribe(function (v) {
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
    UserDetailsComponent.prototype.authenticateUser = function (that, password) {
        that.accountAccessService.authenticate(that.user.accountId, password)
            .subscribe(function (a) {
            console.info("user authenticated successfully");
            sessionStorage.setItem("passwordSet", "true");
            that.initiateChatHub(that);
        }, function (e) {
            console.error("failed to authenticate user", e);
            alert("Failed to authenticate user");
            that.router.navigate(['/']);
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
    UserDetailsComponent.prototype.onDiscloseSecrets = function () {
        this.router.navigate(['reveal-secrets', this.user.accountId]);
    };
    UserDetailsComponent.prototype.initiateO10Hub = function (that) {
        var _this = this;
        this.o10Hub.start().then(function () {
            console.info("Connected to o10Hub");
            _this.o10Hub.invoke("AddToGroup", that.user.o10Id.toString()).then(function () {
                console.info("Added to o10Hub group " + that.user.o10Id);
            }).catch(function (e) {
                console.error(e);
            });
        }).catch(function (e) {
            console.error(e);
            setTimeout(function () { return that.initiateO10Hub(that); }, 1000);
        });
    };
    UserDetailsComponent.prototype.gotoExperts = function () {
        this.router.navigate(['experts-list', this.user.accountId]);
    };
    UserDetailsComponent.prototype.onMyAttributes = function () {
        this.router.navigate(['user-attributes', this.user.accountId]);
    };
    UserDetailsComponent.prototype.gotoQrScan = function () {
        this.router.navigate(['qr-scan', this.user.accountId]);
    };
    UserDetailsComponent.prototype.gotoDuplicate = function () {
        this.router.navigate(['duplicate', this.user.accountId]);
    };
    UserDetailsComponent.prototype.logout = function () {
        sessionStorage.removeItem("passwordSet");
        this.router.navigate(['user-entry']);
    };
    UserDetailsComponent = __decorate([
        (0, core_1.Component)({
            selector: 'app-user-details',
            templateUrl: './user-details.component.html',
            styleUrls: ['./user-details.component.css'],
            encapsulation: core_1.ViewEncapsulation.None,
            animations: [
                (0, animations_1.trigger)('detailExpand', [
                    (0, animations_1.state)('collapsed', (0, animations_1.style)({ height: '0px', minHeight: '0' })),
                    (0, animations_1.state)('expanded', (0, animations_1.style)({ height: '*' })),
                    (0, animations_1.transition)('expanded <=> collapsed', (0, animations_1.animate)('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
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