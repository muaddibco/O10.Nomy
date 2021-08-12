import { Component, OnInit } from '@angular/core';
import { MatRadioChange } from '@angular/material/radio';
import { ActivatedRoute, Router } from '@angular/router';
import { WebcamImage, WebcamInitError, WebcamUtil } from 'ngx-webcam';
import { Observable, Subject } from 'rxjs';
import { ActionDetails } from '../models/action-details';
import { AttributeState } from '../models/attribute-state';
import { UniversalProofsMission } from '../models/universal-proofs-mission';
import { UniversalProofsRequest } from '../models/universal-proofs-request';
import { UserAttribute } from '../models/user-attribute';
import { UserAccessService } from '../user-access.service';

@Component({
  selector: 'app-service-provider',
  templateUrl: './service-provider.component.html',
  styleUrls: ['./service-provider.component.css']
})
export class ServiceProviderComponent implements OnInit {

  public isLoaded = false
  public isBusy = false;
  public userId: number;
  private actionInfo: string;
  public userAttributes: UserAttribute[] = [];
  public selectedAttribute: UserAttribute = null;
  public actionDetails: ActionDetails = null;
  public withValidations: boolean = false;

  public submitted = false;
  public submitClick = false;
  public isError: boolean = false;
  public errorMsg = '';

  // >>=========== Camera ===========================================================
  public allowCameraSwitch = true;
  public multipleWebcamsAvailable = false;
  public isCameraAvailable = false;
  public deviceId: string;
  public errors: WebcamInitError[] = [];
  public isErrorCam: boolean;
  public errorMsgCam: string;
  public toggleCameraText: string;
  public imageContent: string;
  // latest snapshot
  public webcamImage: WebcamImage = null;

  // webcam snapshot trigger
  private trigger: Subject<void> = new Subject<void>();
  // switch to next / previous / specific webcam; true/false: forward/backwards, string: deviceId
  private nextWebcam: Subject<boolean | string> = new Subject<boolean | string>();
  // <<==============================================================================

  constructor(
    private service: UserAccessService,
    private route: ActivatedRoute,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.userId = Number(this.route.snapshot.paramMap.get('userId'))
    this.actionInfo = this.route.snapshot.queryParams['actionInfo'];

    this.service.getUserAttributes(this.userId).subscribe(
      r => {
        this.userAttributes = []

        for (let scheme of r) {
          if (scheme.state == AttributeState.Confirmed) {
            var a = scheme.rootAttributes.find(v => v.state == AttributeState.Confirmed)
            if (a) {
              this.userAttributes.push(a)
            }
          }
        }

        if (this.userAttributes.length === 1) {
          this.selectedAttribute = this.userAttributes[0];
          this.processUserAttributeSelection(this.selectedAttribute);
        }

        this.isLoaded = true
      },
      e => {
        console.error("failed to obtained user attributes", e);
      }
    )

    this.initCam()
  }

  private initCam() {
    WebcamUtil.getAvailableVideoInputs()
      .then((mediaDevices: MediaDeviceInfo[]) => {
        this.multipleWebcamsAvailable = mediaDevices && mediaDevices.length > 1;
        this.isCameraAvailable = mediaDevices && mediaDevices.length > 0;
      });
  }

  private processError(err: any) {
    this.isError = true;
    if (err && err.error && err.error.message) {
      this.errorMsg = err.error.message;
    } else if (err && err.error) {
      this.errorMsg = err.error;
    } else {
      this.errorMsg = err;
    }
  }

  onAttributeSelected(evt: MatRadioChange) {
    let userAttribute: UserAttribute = evt.value;

    this.processUserAttributeSelection(userAttribute);
  }

  private processUserAttributeSelection(userAttribute: UserAttribute) {
    
      this.isBusy = true;
      this.service.getActionDetails(this.userId, this.actionInfo, userAttribute.userAttributeId)
        .subscribe(r => {
          this.actionDetails = r;
          this.withValidations = r.requiredValidations.size > 0
          this.isBusy = false;
        }, err => {
          this.isBusy = false;
          this.processError(err);
        });
    }

  public triggerSnapshot(): void {
    this.trigger.next();
  }

  public showNextWebcam(directionOrDeviceId: boolean | string): void {
    this.nextWebcam.next(directionOrDeviceId);
  }

  public handleImage(webcamImage: WebcamImage): void {
    console.info('received webcam image', webcamImage);
    this.webcamImage = webcamImage;
  }

  public handleInitError(error: WebcamInitError): void {
    this.errors.push(error);
  }

  public cameraWasSwitched(deviceId: string): void {
    console.log('active device: ' + deviceId);
    this.deviceId = deviceId;
  }

  public get triggerObservable(): Observable<void> {
    return this.trigger.asObservable();
  }

  public get nextWebcamObservable(): Observable<boolean | string> {
    return this.nextWebcam.asObservable();
  }

  clearSnapshot() {
    this.webcamImage = null;
  }

  public get userAttributeState(): typeof AttributeState {
    return AttributeState;
  }

  onSubmit() {
    this.submitted = true;

    this.submitClick = true;

    this.processCameraCapture();

    var req: UniversalProofsRequest = {
      mission: UniversalProofsMission.Authentication,
      rootAttributeId: this.selectedAttribute.userAttributeId,
      sessionKey: this.actionDetails.sessionKey,
      target: this.actionDetails.publicKey,
      serviceProviderInfo: this.actionDetails.accountInfo,
      identityPools: []
    }

    this.service.sendUniversalProofs(this.userId, req).subscribe(
      r => {
        console.info("sending universal proofs of authentication succeeded")
      },
      e => {
        console.error("sending universal proofs of authentication failed", e)
      }
    )
  }

  onCancel() {
    this.router.navigate(['/user-details', this.userId]);
  }

  private processCameraCapture() {
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
  }
}
