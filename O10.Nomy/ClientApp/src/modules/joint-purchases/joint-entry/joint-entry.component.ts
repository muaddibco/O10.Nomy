import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { JointPurchasesService } from '../joint-purchases.service';
import { HubConnectionBuilder, HubConnection, LogLevel } from '@microsoft/signalr'

@Component({
  selector: 'app-joint-entry',
  templateUrl: './joint-entry.component.html',
  styleUrls: ['./joint-entry.component.css']
})
export class JointEntryComponent implements OnInit {

  public isLoaded = false
  public isQrLoaded = false
  public loginQrCode: string | null
  private sessionKey: string | null
  public accountId: number
  private o10Hub: HubConnection

  public isError = false
  public isNotEligible = false
  public isProtectionCheckFailed = false

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private serviceAccessor: JointPurchasesService) { }

  ngOnInit(): void {
    var that = this

    this.serviceAccessor.getO10HubUri().subscribe(
      r => {
        console.info("Connecting to O10 Hub with URI " + r["o10HubUri"])

        that.o10Hub = new HubConnectionBuilder()
          .withAutomaticReconnect()
          .configureLogging(LogLevel.Debug)
          .withUrl(r["o10HubUri"])
          .build()

        that.o10Hub.onreconnected(c => {
          this.o10Hub.invoke("AddToGroup", that.sessionKey).then(() => {
            console.info("Added to o10Hub group " + that.sessionKey + " for connection " + that.o10Hub.connectionId);
          }).catch(e => {
            console.error(e);
          });
        });

        that.o10Hub.on("PushSpAuthorizationSucceeded", r => {
          that.router.navigate(['joint-main', r.registrationId, that.sessionKey])
        })

        that.o10Hub.on("PushUserRegistration", r => {
          that.router.navigate(['joint-main', r.registrationId, that.sessionKey])
        })

        that.o10Hub.on("EligibilityCheckFailed", r => {
          that.isNotEligible = true
          that.isError = true
        })

        that.o10Hub.on("ProtectionCheckFailed", r => {
          that.isProtectionCheckFailed = true
          that.isError = true
        })

        that.o10Hub.start().then(() => {
          console.info("Connected to o10Hub");
          that.initComponent(that)
        }).catch(e => {
          console.error(e);
          setTimeout(() => that.initiateO10Hub(that), 1000);
        });
      })
  }

  private initComponent(that: this) {
    that.serviceAccessor.getJointServiceAccount().subscribe(
      r => {
        that.accountId = r.accountId;
        this.initSessionKey(that);
      }
    );
  }

  private initSessionKey(that: this) {
    that.serviceAccessor.getQrCode(that.accountId).subscribe(
      r => {
        that.sessionKey = r.sessionKey;
        that.initiateO10Hub(that);
        that.loginQrCode = r.code;
        that.isError = false
        that.isNotEligible = false
        that.isProtectionCheckFailed = false
        that.isQrLoaded = true;
        that.isLoaded = true;
      },
      e => {
        console.error("failed to initialize session", e);
      }
    );
  }

  initiateO10Hub(that: this) {
    that.o10Hub.invoke("AddToGroup", that.sessionKey).then(() => {
      console.info("Added to o10Hub group " + that.sessionKey + " for connection " + this.o10Hub.connectionId);
    }).catch(e => {
      console.error(e);
    });
  }

  reset() {
    var that = this
    this.o10Hub.invoke("RemoveFromGroup", this.sessionKey).then(() => {
      console.info("Removed from o10Hub group " + this.sessionKey + " for connection " + this.o10Hub.connectionId);
      this.initSessionKey(that);
    }).catch(e => {
      console.error("Failed to remove from o10Hub group " + this.sessionKey + " for connection " + this.o10Hub.connectionId, e);
      this.initSessionKey(that);
    });
  }
}
