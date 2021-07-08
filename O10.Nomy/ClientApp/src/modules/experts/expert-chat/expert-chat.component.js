"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.ExpertChatComponent = void 0;
var core_1 = require("@angular/core");
var signalr_1 = require("@microsoft/signalr");
var ExpertChatComponent = /** @class */ (function () {
    function ExpertChatComponent(expertsAccessService, route) {
        this.expertsAccessService = expertsAccessService;
        this.route = route;
        this.initiatingChat = false;
        this.isChatConfirmed = false;
        this.isLoaded = false;
        this.invoices = [];
    }
    ExpertChatComponent.prototype.ngOnInit = function () {
        var _this = this;
        var expertId = Number(this.route.snapshot.paramMap.get('id'));
        this.userId = Number(this.route.snapshot.paramMap.get('userId'));
        this.paymentsHub = new signalr_1.HubConnectionBuilder()
            .withUrl('/payments')
            .withAutomaticReconnect()
            .build();
        this.paymentsHub.start();
        this.chatHub = new signalr_1.HubConnectionBuilder()
            .withUrl('/chat')
            .withAutomaticReconnect()
            .build();
        this.chatHub.start();
        this.paymentsHub.on("Invoice", function (i) {
            var invoice = i;
            _this.invoices.push(invoice);
            _this.expertsAccessService.payInvoice(_this.userId, invoice.sessionId, invoice.commitment, invoice.currency, invoice.amount);
        });
        this.chatHub.on("Confirmed", function (s) {
            var sessionInfo = s;
            _this.isChatConfirmed = true;
            _this.paymentsHub.invoke("AddToGroup", sessionInfo.sessionId + "_payer");
            _this.expertsAccessService.startChat(_this.expertProfile.expertProfileId, _this.sessionInfo.sessionId).subscribe(function (r) {
            }, function (e) {
            });
        });
        var that = this;
        this.expertsAccessService.getExpert(expertId).subscribe(function (r) {
            _this.isLoaded = true;
            _this.expertProfile = r;
            _this.expertsAccessService.initiateChatSession().subscribe(function (s) {
                that.sessionInfo = s;
                that.chatHub.invoke("AddToGroup", s.sessionId);
                that.initiatingChat = true;
                that.expertsAccessService.inviteToChat(that.expertProfile.expertProfileId, s.sessionId).subscribe(function (a) {
                }, function (e) {
                    console.error(e);
                });
            }, function (e) {
                console.error(e);
            });
        }, function (e) {
            console.error(e);
        });
    };
    ExpertChatComponent = __decorate([
        core_1.Component({
            selector: 'app-expert-chat',
            templateUrl: './expert-chat.component.html',
            styleUrls: ['./expert-chat.component.css']
        })
    ], ExpertChatComponent);
    return ExpertChatComponent;
}());
exports.ExpertChatComponent = ExpertChatComponent;
//# sourceMappingURL=expert-chat.component.js.map