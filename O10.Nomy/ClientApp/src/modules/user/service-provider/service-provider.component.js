"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.ServiceProviderComponent = void 0;
var core_1 = require("@angular/core");
var ngx_webcam_1 = require("ngx-webcam");
var rxjs_1 = require("rxjs");
var attribute_state_1 = require("../models/attribute-state");
var universal_proofs_mission_1 = require("../models/universal-proofs-mission");
var ServiceProviderComponent = /** @class */ (function () {
    // <<==============================================================================
    function ServiceProviderComponent(service, route, router) {
        this.service = service;
        this.route = route;
        this.router = router;
        this.isLoaded = false;
        this.isBusy = false;
        this.userAttributes = [];
        this.selectedAttribute = null;
        this.actionDetails = null;
        this.withValidations = false;
        this.submitted = false;
        this.submitClick = false;
        this.isError = false;
        this.errorMsg = '';
        // >>=========== Camera ===========================================================
        this.allowCameraSwitch = true;
        this.multipleWebcamsAvailable = false;
        this.isCameraAvailable = false;
        this.errors = [];
        // latest snapshot
        this.webcamImage = null;
        // webcam snapshot trigger
        this.trigger = new rxjs_1.Subject();
        // switch to next / previous / specific webcam; true/false: forward/backwards, string: deviceId
        this.nextWebcam = new rxjs_1.Subject();
    }
    ServiceProviderComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.userId = Number(this.route.snapshot.paramMap.get('userId'));
        this.actionInfo = this.route.snapshot.queryParams['actionInfo'];
        this.service.getUserAttributes(this.userId).subscribe(function (r) {
            _this.userAttributes = [];
            for (var _i = 0, r_1 = r; _i < r_1.length; _i++) {
                var scheme = r_1[_i];
                if (scheme.state == attribute_state_1.AttributeState.Confirmed) {
                    var a = scheme.rootAttributes.find(function (v) { return v.state == attribute_state_1.AttributeState.Confirmed; });
                    if (a) {
                        _this.userAttributes.push(a);
                    }
                }
            }
            if (_this.userAttributes.length === 1) {
                _this.selectedAttribute = _this.userAttributes[0];
                _this.processUserAttributeSelection(_this.selectedAttribute);
            }
            _this.isLoaded = true;
        }, function (e) {
            console.error("failed to obtained user attributes", e);
        });
        this.initCam();
    };
    ServiceProviderComponent.prototype.initCam = function () {
        var _this = this;
        ngx_webcam_1.WebcamUtil.getAvailableVideoInputs()
            .then(function (mediaDevices) {
            _this.multipleWebcamsAvailable = mediaDevices && mediaDevices.length > 1;
            _this.isCameraAvailable = mediaDevices && mediaDevices.length > 0;
        });
    };
    ServiceProviderComponent.prototype.processError = function (err) {
        this.isError = true;
        if (err && err.error && err.error.message) {
            this.errorMsg = err.error.message;
        }
        else if (err && err.error) {
            this.errorMsg = err.error;
        }
        else {
            this.errorMsg = err;
        }
    };
    ServiceProviderComponent.prototype.onAttributeSelected = function (evt) {
        var userAttribute = evt.value;
        this.processUserAttributeSelection(userAttribute);
    };
    ServiceProviderComponent.prototype.processUserAttributeSelection = function (userAttribute) {
        var _this = this;
        this.isBusy = true;
        this.service.getActionDetails(this.userId, this.actionInfo, userAttribute.userAttributeId)
            .subscribe(function (r) {
            _this.actionDetails = r;
            _this.withValidations = r.requiredValidations.size > 0;
            _this.isBusy = false;
        }, function (err) {
            _this.isBusy = false;
            _this.processError(err);
        });
    };
    ServiceProviderComponent.prototype.triggerSnapshot = function () {
        this.trigger.next();
    };
    ServiceProviderComponent.prototype.showNextWebcam = function (directionOrDeviceId) {
        this.nextWebcam.next(directionOrDeviceId);
    };
    ServiceProviderComponent.prototype.handleImage = function (webcamImage) {
        console.info('received webcam image', webcamImage);
        this.webcamImage = webcamImage;
    };
    ServiceProviderComponent.prototype.handleInitError = function (error) {
        this.errors.push(error);
    };
    ServiceProviderComponent.prototype.cameraWasSwitched = function (deviceId) {
        console.log('active device: ' + deviceId);
        this.deviceId = deviceId;
    };
    Object.defineProperty(ServiceProviderComponent.prototype, "triggerObservable", {
        get: function () {
            return this.trigger.asObservable();
        },
        enumerable: false,
        configurable: true
    });
    Object.defineProperty(ServiceProviderComponent.prototype, "nextWebcamObservable", {
        get: function () {
            return this.nextWebcam.asObservable();
        },
        enumerable: false,
        configurable: true
    });
    ServiceProviderComponent.prototype.clearSnapshot = function () {
        this.webcamImage = null;
    };
    Object.defineProperty(ServiceProviderComponent.prototype, "userAttributeState", {
        get: function () {
            return attribute_state_1.AttributeState;
        },
        enumerable: false,
        configurable: true
    });
    ServiceProviderComponent.prototype.onSubmit = function () {
        var _this = this;
        this.submitted = true;
        this.submitClick = true;
        this.processCameraCapture();
        var req = {
            mission: universal_proofs_mission_1.UniversalProofsMission.Authentication,
            rootAttributeId: this.selectedAttribute.userAttributeId,
            sessionKey: this.actionDetails.sessionKey,
            target: this.actionDetails.publicKey,
            serviceProviderInfo: this.actionDetails.accountInfo,
            identityPools: []
        };
        this.service.sendUniversalProofs(this.userId, req).subscribe(function (r) {
            console.info("sending universal proofs of authentication succeeded");
            _this.router.navigate(['/user-details', _this.userId]);
        }, function (e) {
            console.error("sending universal proofs of authentication failed", e);
            _this.router.navigate(['/user-details', _this.userId]);
        });
    };
    ServiceProviderComponent.prototype.onCancel = function () {
        this.router.navigate(['/user-details', this.userId]);
    };
    ServiceProviderComponent.prototype.processCameraCapture = function () {
        if (this.actionDetails.isBiometryRequired) {
            if (this.webcamImage == null) {
                this.errorMsgCam = "No image captured!";
                this.isErrorCam = true;
            }
            else {
                this.imageContent = this.webcamImage.imageAsBase64;
            }
        }
        else {
            this.imageContent = null;
        }
    };
    ServiceProviderComponent = __decorate([
        (0, core_1.Component)({
            selector: 'app-service-provider',
            templateUrl: './service-provider.component.html',
            styleUrls: ['./service-provider.component.css']
        })
    ], ServiceProviderComponent);
    return ServiceProviderComponent;
}());
exports.ServiceProviderComponent = ServiceProviderComponent;
//# sourceMappingURL=service-provider.component.js.map