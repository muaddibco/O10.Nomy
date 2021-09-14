"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.CompromizedComponent = void 0;
var core_1 = require("@angular/core");
var CompromizedComponent = /** @class */ (function () {
    function CompromizedComponent(appState, router, route) {
        this.appState = appState;
        this.router = router;
        this.route = route;
    }
    CompromizedComponent.prototype.ngOnInit = function () {
        this.appState.setIsMobile(true);
        var userId = Number(this.route.snapshot.paramMap.get('userId'));
    };
    CompromizedComponent = __decorate([
        (0, core_1.Component)({
            selector: 'app-compromized',
            templateUrl: './compromized.component.html',
            styleUrls: ['./compromized.component.css']
        })
    ], CompromizedComponent);
    return CompromizedComponent;
}());
exports.CompromizedComponent = CompromizedComponent;
//# sourceMappingURL=compromized.component.js.map