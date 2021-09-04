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
exports.UserAttributesComponent = void 0;
var animations_1 = require("@angular/animations");
var core_1 = require("@angular/core");
var collections_1 = require("@angular/cdk/collections");
var rxjs_1 = require("rxjs");
var UserAttributesComponent = /** @class */ (function () {
    function UserAttributesComponent(attributesService, appState, router, route) {
        this.attributesService = attributesService;
        this.appState = appState;
        this.router = router;
        this.route = route;
        this.isLoaded = false;
        this.attributeSchemes = [];
        this.dataSource = new AttributeSchemesDataSource(this.attributeSchemes);
        this.displayedColumns = ['schemeName', 'rootAttributeContent', 'issuerName'];
        this.associatedAttrsDisplayedColumns = ['alias', 'content'];
    }
    UserAttributesComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.appState.setIsMobile(true);
        this.userId = Number(this.route.snapshot.paramMap.get('userId'));
        this.attributesService.getUserAttributes(this.userId).subscribe(function (r) {
            _this.attributeSchemes = r;
            for (var _i = 0, _a = _this.attributeSchemes; _i < _a.length; _i++) {
                var attrScheme = _a[_i];
                var rootIdx = attrScheme.associatedSchemes.findIndex(function (v) { return v.issuerAddress === attrScheme.issuerAddress; });
                if (rootIdx >= 0) {
                    attrScheme.rootAssociatedScheme = attrScheme.associatedSchemes.splice(rootIdx, 1)[0];
                }
            }
            _this.dataSource.setData(_this.attributeSchemes);
            _this.isLoaded = true;
        });
    };
    UserAttributesComponent.prototype.onBack = function () {
        this.router.navigate(['/user-details', this.userId]);
    };
    UserAttributesComponent = __decorate([
        (0, core_1.Component)({
            selector: 'app-user-attributes',
            templateUrl: './user-attributes.component.html',
            styleUrls: ['./user-attributes.component.css'],
            encapsulation: core_1.ViewEncapsulation.None,
            animations: [
                (0, animations_1.trigger)('detailExpand', [
                    (0, animations_1.state)('collapsed', (0, animations_1.style)({ height: '0px', minHeight: '0' })),
                    (0, animations_1.state)('expanded', (0, animations_1.style)({ height: '*' })),
                    (0, animations_1.transition)('expanded <=> collapsed', (0, animations_1.animate)('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
                ])
            ]
        })
    ], UserAttributesComponent);
    return UserAttributesComponent;
}());
exports.UserAttributesComponent = UserAttributesComponent;
var AttributeSchemesDataSource = /** @class */ (function (_super) {
    __extends(AttributeSchemesDataSource, _super);
    function AttributeSchemesDataSource(initialData) {
        var _this = _super.call(this) || this;
        _this._dataStream = new rxjs_1.ReplaySubject();
        _this.setData(initialData);
        return _this;
    }
    AttributeSchemesDataSource.prototype.connect = function () {
        return this._dataStream;
    };
    AttributeSchemesDataSource.prototype.disconnect = function () { };
    AttributeSchemesDataSource.prototype.setData = function (data) {
        this._dataStream.next(data);
    };
    return AttributeSchemesDataSource;
}(collections_1.DataSource));
//# sourceMappingURL=user-attributes.component.js.map