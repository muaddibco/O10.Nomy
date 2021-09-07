"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.AccountsAccessService = void 0;
var core_1 = require("@angular/core");
var AccountsAccessService = /** @class */ (function () {
    function AccountsAccessService(http) {
        this.http = http;
    }
    AccountsAccessService.prototype.getAccountById = function (accountId) {
        return this.http.get('/api/accounts/' + accountId);
    };
    AccountsAccessService.prototype.find = function (alias) {
        return this.http.get('/api/accounts/find', { params: { "accountAlias": alias } });
    };
    AccountsAccessService.prototype.authenticate = function (accountId, password) {
        return this.http.post('/api/accounts/' + accountId + '/auth', { password: password });
    };
    AccountsAccessService.prototype.duplicate = function (accountId, newEmail) {
        return this.http.post('/api/accounts/' + accountId + '/duplicate', { newEmail: newEmail });
    };
    AccountsAccessService.prototype.isAuthenticated = function (accountId) {
        return this.http.get('/api/accounts/' + accountId + '/auth');
    };
    AccountsAccessService = __decorate([
        (0, core_1.Injectable)({
            providedIn: 'root'
        })
    ], AccountsAccessService);
    return AccountsAccessService;
}());
exports.AccountsAccessService = AccountsAccessService;
//# sourceMappingURL=accounts-access.service.js.map