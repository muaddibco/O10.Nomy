"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.JointEntryComponent = void 0;
var core_1 = require("@angular/core");
var signalr_1 = require("@microsoft/signalr");
var JointEntryComponent = /** @class */ (function () {
    function JointEntryComponent(route, router, serviceAccessor) {
        this.route = route;
        this.router = router;
        this.serviceAccessor = serviceAccessor;
        this.isLoaded = false;
        this.isQrLoaded = false;
    }
    JointEntryComponent.prototype.ngOnInit = function () {
        var that = this;
        this.serviceAccessor.getO10HubUri().subscribe(function (r) {
            console.info("Connecting to O10 Hub with URI " + r["o10HubUri"]);
            that.o10Hub = new signalr_1.HubConnectionBuilder()
                .withUrl(r["o10HubUri"])
                .build();
            that.o10Hub.on("PushSpAuthorizationSucceeded", function () {
                that.router.navigate(['joint-main', that.sessionKey]);
            });
            that.serviceAccessor.getJointServiceAccount().subscribe(function (r) {
                that.accountId = r.accountId;
                that.serviceAccessor.getQrCode(that.accountId).subscribe(function (r) {
                    that.sessionKey = r.sessionKey;
                    that.initiateO10Hub(that);
                    that.loginQrCode = r.code;
                    that.isQrLoaded = true;
                    that.isLoaded = true;
                }, function (e) {
                    console.error("failed to initialize session", e);
                });
            });
        });
    };
    JointEntryComponent.prototype.initiateO10Hub = function (that) {
        var _this = this;
        this.o10Hub.start().then(function () {
            console.info("Connected to o10Hub");
            _this.o10Hub.invoke("AddToGroup", that.sessionKey).then(function () {
                console.info("Added to o10Hub group " + that.sessionKey);
            }).catch(function (e) {
                console.error(e);
            });
        }).catch(function (e) {
            console.error(e);
            setTimeout(function () { return that.initiateO10Hub(that); }, 1000);
        });
    };
    JointEntryComponent = __decorate([
        core_1.Component({
            selector: 'app-joint-entry',
            templateUrl: './joint-entry.component.html',
            styleUrls: ['./joint-entry.component.css']
        })
    ], JointEntryComponent);
    return JointEntryComponent;
}());
exports.JointEntryComponent = JointEntryComponent;
//# sourceMappingURL=joint-entry.component.js.map