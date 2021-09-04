"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.JointPurchasesService = void 0;
var core_1 = require("@angular/core");
var JointPurchasesService = /** @class */ (function () {
    function JointPurchasesService(http) {
        this.http = http;
    }
    JointPurchasesService.prototype.getJointServiceAccount = function () {
        return this.http.get('/api/JointService');
    };
    JointPurchasesService.prototype.getQrCode = function (id) {
        return this.http.get('/api/ServiceProviders/SessionInfo', { params: { accountId: id } });
    };
    JointPurchasesService.prototype.getO10HubUri = function () {
        return this.http.get('/api/JointService/O10Hub');
    };
    JointPurchasesService.prototype.addJointGroup = function (o10RegistrationId, name, description) {
        return this.http.post('/api/JointService/' + o10RegistrationId + '/JointGroup', { name: name, description: description });
    };
    JointPurchasesService.prototype.getJointGroups = function (o10RegistrationId) {
        return this.http.get('/api/JointService/' + o10RegistrationId + '/JointGroups');
    };
    JointPurchasesService.prototype.getJointGroupMembers = function (groupId) {
        return this.http.get('/api/JointService/JointGroup/' + groupId + '/Members');
    };
    JointPurchasesService.prototype.addJointGroupMember = function (groupId, email, description) {
        return this.http.post('/api/JointService/JointGroup/' + groupId + '/Member', { email: email, description: description });
    };
    JointPurchasesService = __decorate([
        core_1.Injectable({
            providedIn: 'root'
        })
    ], JointPurchasesService);
    return JointPurchasesService;
}());
exports.JointPurchasesService = JointPurchasesService;
//# sourceMappingURL=joint-purchases.service.js.map