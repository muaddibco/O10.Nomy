"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.ExpertsListComponent = void 0;
var core_1 = require("@angular/core");
var ExpertsListComponent = /** @class */ (function () {
    function ExpertsListComponent(expertsAccessService, route, router) {
        this.expertsAccessService = expertsAccessService;
        this.route = route;
        this.router = router;
        this.isLoaded = false;
        this.expertiseAreas = null;
    }
    ExpertsListComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.userId = Number(this.route.snapshot.paramMap.get('userId'));
        this.expertsAccessService.getAllExpertiseAreas().subscribe(function (r) {
            _this.expertiseAreas = r;
            _this.isLoaded = true;
        }, function (e) {
            console.error(e);
        });
    };
    ExpertsListComponent.prototype.gotoExpert = function (expertProfile) {
        this.router.navigate(['expert-chat', this.userId, expertProfile.expertProfileId]);
    };
    ExpertsListComponent = __decorate([
        core_1.Component({
            selector: 'app-experts-list',
            templateUrl: './experts-list.component.html',
            styleUrls: ['./experts-list.component.css']
        })
    ], ExpertsListComponent);
    return ExpertsListComponent;
}());
exports.ExpertsListComponent = ExpertsListComponent;
//# sourceMappingURL=experts-list.component.js.map