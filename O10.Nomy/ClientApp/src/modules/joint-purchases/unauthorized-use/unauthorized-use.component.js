"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.UnauthorizedUseComponent = void 0;
var core_1 = require("@angular/core");
var rxjs_1 = require("rxjs");
var operators_1 = require("rxjs/operators");
var UnauthorizedUseComponent = /** @class */ (function () {
    function UnauthorizedUseComponent(http, router) {
        this.http = http;
        this.router = router;
        this.ipaddress = '';
        this.latitude = '';
        this.longitude = '';
        this.currency = '';
        this.currencysymbol = '';
        this.isp = '';
        this.city = '';
        this.country = '';
    }
    UnauthorizedUseComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.getLocation();
        this.getIpAddress().subscribe(function (res) {
            _this.ipaddress = res['ip'];
            _this.getGEOLocation(_this.ipaddress).subscribe(function (res) {
                _this.latitude = res['latitude'];
                _this.longitude = res['longitude'];
                _this.currency = res['currency']['code'];
                _this.currencysymbol = res['currency']['symbol'];
                _this.city = res['city'];
                _this.country = res['country_code3'];
                _this.isp = res['isp'];
                console.log(res);
            });
            //console.log(res);
        });
    };
    UnauthorizedUseComponent.prototype.getLocation = function () {
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(function (position) {
                var longitude = position.coords.longitude;
                var latitude = position.coords.latitude;
                console.info("longitude = " + longitude + "; latitude = " + latitude);
            });
        }
        else {
            console.error("No support for geolocation");
        }
    };
    UnauthorizedUseComponent.prototype.getIpAddress = function () {
        return this.http
            .get('https://api.ipify.org/?format=json')
            .pipe((0, operators_1.catchError)(this.handleError));
    };
    UnauthorizedUseComponent.prototype.getGEOLocation = function (ip) {
        // Update your api key to get from https://ipgeolocation.io
        var url = "https://api.ipgeolocation.io/ipgeo?apiKey=11ae072767fc45ecb9ff83d13692ae2d&ip=" + ip;
        return this.http
            .get(url)
            .pipe((0, operators_1.catchError)(this.handleError));
    };
    UnauthorizedUseComponent.prototype.handleError = function (error) {
        if (error.error instanceof ErrorEvent) {
            // A client-side or network error occurred. Handle it accordingly.
            console.error('An error occurred:', error.error.message);
        }
        else {
            // The backend returned an unsuccessful response code.
            // The response body may contain clues as to what went wrong,
            console.error("Backend returned code " + error.status + ", " +
                ("body was: " + error.error));
        }
        // return an observable with a user-facing error message
        return (0, rxjs_1.throwError)('Something bad happened; please try again later.');
    };
    UnauthorizedUseComponent.prototype.onDismiss = function () {
        this.router.navigate(['joint-purchases']);
    };
    UnauthorizedUseComponent = __decorate([
        (0, core_1.Component)({
            selector: 'app-unauthorized-use',
            templateUrl: './unauthorized-use.component.html',
            styleUrls: ['./unauthorized-use.component.css']
        })
    ], UnauthorizedUseComponent);
    return UnauthorizedUseComponent;
}());
exports.UnauthorizedUseComponent = UnauthorizedUseComponent;
//# sourceMappingURL=unauthorized-use.component.js.map