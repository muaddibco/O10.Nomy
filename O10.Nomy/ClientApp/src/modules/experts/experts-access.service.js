"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.ExpertsAccessService = void 0;
var core_1 = require("@angular/core");
var ExpertsAccessService = /** @class */ (function () {
    function ExpertsAccessService(http) {
        this.http = http;
    }
    ExpertsAccessService.prototype.getAllExpertiseAreas = function () {
        return this.http.get('/api/experts');
    };
    ExpertsAccessService.prototype.getExpert = function (expertProfileId) {
        return this.http.get('/api/experts/' + expertProfileId);
    };
    ExpertsAccessService.prototype.initiateChatSession = function () {
        return this.http.post('/api/experts/session', null);
    };
    ExpertsAccessService.prototype.startChat = function (expertProfileId, sessionId) {
        return this.http.post('/api/experts/' + expertProfileId + '/session/' + sessionId, null);
    };
    ExpertsAccessService.prototype.inviteToChat = function (expertProfileId, sessionId) {
        return this.http.post('/api/experts/' + expertProfileId + '/chat', null, {
            params: {
                sessionId: sessionId
            }
        });
    };
    ExpertsAccessService.prototype.payInvoice = function (userId, sessionId, invoiceCommitment, currency, amount) {
        return this.http.post('/api/user/' + userId + "/pay", { sessionId: sessionId, invoiceCommitment: invoiceCommitment, currency: currency, amount: amount });
    };
    ExpertsAccessService = __decorate([
        core_1.Injectable({
            providedIn: 'root'
        })
    ], ExpertsAccessService);
    return ExpertsAccessService;
}());
exports.ExpertsAccessService = ExpertsAccessService;
//# sourceMappingURL=experts-access.service.js.map