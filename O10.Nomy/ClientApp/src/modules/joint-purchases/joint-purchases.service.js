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
    JointPurchasesService = __decorate([
        core_1.Injectable({
            providedIn: 'root'
        })
    ], JointPurchasesService);
    return JointPurchasesService;
}());
exports.JointPurchasesService = JointPurchasesService;
//# sourceMappingURL=joint-purchases.service.js.map