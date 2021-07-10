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
Object.defineProperty(exports, "__esModule", { value: true });
exports.ExpertChatComponent = void 0;
var core_1 = require("@angular/core");
var signalr_1 = require("@microsoft/signalr");
var collections_1 = require("@angular/cdk/collections");
var rxjs_1 = require("rxjs");
var animations_1 = require("@angular/animations");
var ExpertChatComponent = /** @class */ (function () {
    function ExpertChatComponent(expertsAccessService, route) {
        this.expertsAccessService = expertsAccessService;
        this.route = route;
        this.displayedColumns = ['commitment'];
        this.initiatingChat = false;
        this.isChatConfirmed = false;
        this.isLoaded = false;
        this.invoices = [];
        this.dataSource = new InvoicesDataSource(this.invoices);
    }
    ExpertChatComponent.prototype.ngOnInit = function () {
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
        var that = this;
        this.chatHub.start().then(function () {
            console.info("chatHub started, getting expert info for expertId " + expertId);
            that.expertsAccessService.getExpert(expertId).subscribe(function (r) {
                that.isLoaded = true;
                that.expertProfile = r;
                console.info("Intiating chat session with the expert " + r.firstName + " " + r.lastName);
                that.expertsAccessService.initiateChatSession().subscribe(function (s) {
                    console.info("Chat session with sessionId " + s.sessionId + " initiated");
                    that.sessionInfo = s;
                    that.chatHub.invoke("AddToGroup", s.sessionId);
                    that.initiatingChat = true;
                    console.info("Sending chat invitation to " + that.expertProfile.firstName + " " + that.expertProfile.lastName);
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
        });
        this.paymentsHub.on("Invoice", function (i) {
            console.info("Received invoice " + JSON.stringify(i));
            var invoice = i;
            that.invoices.push(invoice);
            that.dataSource.setData(that.invoices);
            console.info("Pay invoice " + invoice.commitment);
            that.expertsAccessService.payInvoice(that.userId, that.sessionInfo.sessionId, invoice.commitment, "USD", that.expertProfile.fee).subscribe(function (r) {
                console.info("Invoice " + invoice.commitment + " paid with payment " + r.commitment);
            }, function (e) {
                console.error("Failed to pay invoice " + invoice.commitment, e);
            });
        });
        this.chatHub.on("Confirmed", function (s) {
            var sessionInfo = s;
            that.isChatConfirmed = true;
            console.info("Confirmation for session " + sessionInfo.sessionId + " from " + that.expertProfile.firstName + " " + that.expertProfile.lastName + " received, adding to group " + sessionInfo.sessionId + "_Payer");
            that.paymentsHub.invoke("AddToGroup", sessionInfo.sessionId + "_Payer").then(function () {
                console.info("Added to group " + sessionInfo.sessionId + "_Payer, starting chat...");
                that.expertsAccessService.startChat(that.expertProfile.expertProfileId, that.sessionInfo.sessionId).subscribe(function (r) {
                }, function (e) {
                });
            });
        });
    };
    ExpertChatComponent = __decorate([
        core_1.Component({
            selector: 'app-expert-chat',
            templateUrl: './expert-chat.component.html',
            styleUrls: ['./expert-chat.component.css'],
            encapsulation: core_1.ViewEncapsulation.None,
            animations: [
                animations_1.trigger('detailExpand', [
                    animations_1.state('collapsed', animations_1.style({ height: '0px', minHeight: '0' })),
                    animations_1.state('expanded', animations_1.style({ height: '*' })),
                    animations_1.transition('expanded <=> collapsed', animations_1.animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
                ]),
            ],
        })
    ], ExpertChatComponent);
    return ExpertChatComponent;
}());
exports.ExpertChatComponent = ExpertChatComponent;
var InvoicesDataSource = /** @class */ (function (_super) {
    __extends(InvoicesDataSource, _super);
    function InvoicesDataSource(initialData) {
        var _this = _super.call(this) || this;
        _this._dataStream = new rxjs_1.ReplaySubject();
        _this.setData(initialData);
        return _this;
    }
    InvoicesDataSource.prototype.connect = function () {
        return this._dataStream;
    };
    InvoicesDataSource.prototype.disconnect = function () { };
    InvoicesDataSource.prototype.setData = function (data) {
        this._dataStream.next(data);
    };
    return InvoicesDataSource;
}(collections_1.DataSource));
//# sourceMappingURL=expert-chat.component.js.map