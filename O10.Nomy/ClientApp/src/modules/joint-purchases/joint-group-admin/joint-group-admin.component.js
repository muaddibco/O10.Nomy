"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.JointGroupAdminComponent = void 0;
var core_1 = require("@angular/core");
var signalr_1 = require("@microsoft/signalr");
var add_jointgroupmember_dialog_1 = require("../add-jointgroupmember-dialog/add-jointgroupmember.dialog");
var JointGroupAdminComponent = /** @class */ (function () {
    function JointGroupAdminComponent(route, router, serviceAccessor, dialog) {
        this.route = route;
        this.router = router;
        this.serviceAccessor = serviceAccessor;
        this.dialog = dialog;
        this.isLoaded = false;
        this.groupMembers = [];
    }
    JointGroupAdminComponent.prototype.ngOnInit = function () {
        this.groupId = Number(this.route.snapshot.paramMap.get('groupId'));
        this.registrationId = Number(this.route.snapshot.paramMap.get('registrationId'));
        this.sessionKey = this.route.snapshot.paramMap.get('sessionKey');
        var that = this;
        this.serviceAccessor.getJointGroupMembers(this.groupId).subscribe(function (r) {
            that.groupMembers = r;
        });
        this.serviceAccessor.getO10HubUri().subscribe(function (r) {
            console.info("Connecting to O10 Hub with URI " + r["o10HubUri"]);
            that.o10Hub = new signalr_1.HubConnectionBuilder()
                .withUrl(r["o10HubUri"])
                .build();
            that.initiateO10Hub(that);
            that.isLoaded = true;
        });
    };
    JointGroupAdminComponent.prototype.initiateO10Hub = function (that) {
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
    JointGroupAdminComponent.prototype.addNewMember = function () {
        var _this = this;
        var dialogRef = this.dialog.open(add_jointgroupmember_dialog_1.AddJointGroupMemberDialog);
        dialogRef.afterClosed().subscribe(function (r) {
            if (r) {
                _this.serviceAccessor.addJointGroupMember(_this.groupId, r.email, r.description).subscribe(function (r) {
                    _this.groupMembers.push(r);
                }, function (e) {
                    console.error("Failed to add a joint groups member with email " + r.email + " and description " + r.description, e);
                });
            }
        });
    };
    JointGroupAdminComponent = __decorate([
        core_1.Component({
            selector: 'app-joint-group-admin',
            templateUrl: './joint-group-admin.component.html',
            styleUrls: ['./joint-group-admin.component.css']
        })
    ], JointGroupAdminComponent);
    return JointGroupAdminComponent;
}());
exports.JointGroupAdminComponent = JointGroupAdminComponent;
//# sourceMappingURL=joint-group-admin.component.js.map