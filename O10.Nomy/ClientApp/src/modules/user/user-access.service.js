"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.UserAccessService = void 0;
var core_1 = require("@angular/core");
var UserAccessService = /** @class */ (function () {
    function UserAccessService(http) {
        this.http = http;
    }
    UserAccessService.prototype.register = function (user) {
        return this.http.post('/api/accounts', user);
    };
    UserAccessService.prototype.getUserAttributes = function (accountId) {
        return this.http.get('/api/user/' + accountId + '/attributes');
    };
    UserAccessService.prototype.getUserDetails = function (accountId) {
        return this.http.get('/api/user/' + accountId);
    };
    UserAccessService.prototype.confirmSession = function (sessionId) {
        return this.http.post('/api/user/session/' + sessionId, null);
    };
    UserAccessService.prototype.sendInvoice = function (accountId, sessionId, amount, currency) {
        return this.http.post('/api/user/' + accountId + '/invoice', { sessionId: sessionId, amount: amount, currency: currency });
    };
    UserAccessService.prototype.getActionInfo = function (actionInfo) {
        return this.http.get('/api/user/ActionInfo', { params: { actionInfo: actionInfo } });
    };
    UserAccessService.prototype.getActionDetails = function (accountId, actionInfo, attributeId) {
        return this.http.get('/api/user/' + accountId + '/ActionDetails', { params: { actionInfo: actionInfo, userAttributeId: attributeId } });
    };
    UserAccessService.prototype.sendUniversalProofs = function (accountId, universalProofsRequest) {
        return this.http.post('/api/user/' + accountId + '/UniversalProofs', universalProofsRequest);
    };
    UserAccessService.prototype.getDisclosedSecrets = function (accountId, password) {
        return this.http.get('/api/user/' + accountId + '/secrets?password=' + password);
    };
    UserAccessService = __decorate([
        (0, core_1.Injectable)({
            providedIn: 'root'
        })
    ], UserAccessService);
    return UserAccessService;
}());
exports.UserAccessService = UserAccessService;
//# sourceMappingURL=user-access.service.js.map