"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.AppComponent = void 0;
var core_1 = require("@angular/core");
var AppComponent = /** @class */ (function () {
    function AppComponent(router, appState, changeDetector) {
        var _this = this;
        this.router = router;
        this.appState = appState;
        this.changeDetector = changeDetector;
        this.title = 'app';
        this.isMobile = false;
        this.showNavMenu = false;
        this.appState.StateChanged.subscribe(function (s) {
            _this.isMobile = s.isMobile;
            _this.showNavMenu = !s.isMobile;
            _this.changeDetector.detectChanges();
        });
    }
    AppComponent.prototype.ngOnInit = function () {
        var ua = navigator.userAgent;
        if (/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini|Mobile|mobile|CriOS/i.test(ua)) {
            this.showNavMenu = false;
            this.isMobile = true;
        }
        else {
            this.showNavMenu = true;
            this.isMobile = false;
        }
    };
    AppComponent.prototype.onIsMobileChangedHandler = function (isMobile) {
        this.isMobile = isMobile;
        this.showNavMenu = !isMobile;
        if (isMobile) {
            this.appState.setIsMobile(isMobile);
        }
    };
    AppComponent = __decorate([
        (0, core_1.Component)({
            selector: 'app-root',
            templateUrl: './app.component.html',
            styleUrls: ['./app.component.css'],
            encapsulation: core_1.ViewEncapsulation.None
        })
    ], AppComponent);
    return AppComponent;
}());
exports.AppComponent = AppComponent;
//# sourceMappingURL=app.component.js.map