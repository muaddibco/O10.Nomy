"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.JointMainComponent = void 0;
var core_1 = require("@angular/core");
var signalr_1 = require("@microsoft/signalr");
var add_jointgroup_dialog_1 = require("../add-jointgroup-dialog/add-jointgroup.dialog");
var JointMainComponent = /** @class */ (function () {
    function JointMainComponent(route, router, serviceAccessor, dialog) {
        this.route = route;
        this.router = router;
        this.serviceAccessor = serviceAccessor;
        this.dialog = dialog;
        this.isLoaded = false;
        this.groups = [];
    }
    JointMainComponent.prototype.ngOnInit = function () {
        this.registrationId = Number(this.route.snapshot.paramMap.get('registrationId'));
        this.sessionKey = this.route.snapshot.paramMap.get('sessionKey');
        var that = this;
        this.serviceAccessor.getO10HubUri().subscribe(function (r) {
            console.info("Connecting to O10 Hub with URI " + r["o10HubUri"]);
            that.o10Hub = new signalr_1.HubConnectionBuilder()
                .withUrl(r["o10HubUri"])
                .build();
            that.initiateO10Hub(that);
            that.isLoaded = true;
        });
    };
    JointMainComponent.prototype.initiateO10Hub = function (that) {
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
    JointMainComponent.prototype.addNewGroup = function () {
        var _this = this;
        var dialogRef = this.dialog.open(add_jointgroup_dialog_1.AddJointGroupDialog);
        dialogRef.afterClosed().subscribe(function (r) {
            if (r) {
                _this.serviceAccessor.addJointGroup(_this.registrationId, r.name, r.description).subscribe(function (r) {
                    _this.groups.push(r);
                }, function (e) {
                    console.error("Failed to add a joint groups with name " + r.name + " and description " + r.description, e);
                });
            }
        });
    };
    JointMainComponent = __decorate([
        core_1.Component({
            selector: 'app-joint-main',
            templateUrl: './joint-main.component.html',
            styleUrls: ['./joint-main.component.css']
        })
    ], JointMainComponent);
    return JointMainComponent;
}());
exports.JointMainComponent = JointMainComponent;
//# sourceMappingURL=joint-main.component.js.map