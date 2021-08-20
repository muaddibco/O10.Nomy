import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { JointPurchasesService } from '../joint-purchases.service';
import { HubConnectionBuilder, HubConnection } from '@microsoft/signalr'

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

  constructor(private route: ActivatedRoute, private router: Router, private serviceAccessor: JointPurchasesService) { }

  ngOnInit(): void {
    var that = this

    this.serviceAccessor.getO10HubUri().subscribe(
      r => {
        console.info("Connecting to O10 Hub with URI " + r["o10HubUri"])

        that.o10Hub = new HubConnectionBuilder()
          .withUrl(r["o10HubUri"])
          .build()

        that.o10Hub.on("PushSpAuthorizationSucceeded", () => {
          that.router.navigate(['joint-main', that.sessionKey])
        })

        that.serviceAccessor.getJointServiceAccount().subscribe(
          r => {
            that.accountId = r.accountId
            that.serviceAccessor.getQrCode(that.accountId).subscribe(
              r => {
                that.sessionKey = r.sessionKey
                that.initiateO10Hub(that)
                that.loginQrCode = r.code
                that.isQrLoaded = true
                that.isLoaded = true
              },
              e => {
                console.error("failed to initialize session", e)
              }
            )
          }
        )
      })
  }

  private initiateO10Hub(that: this) {
    this.o10Hub.start().then(() => {
      console.info("Connected to o10Hub");
      this.o10Hub.invoke("AddToGroup", that.sessionKey).then(() => {
        console.info("Added to o10Hub group " + that.sessionKey);
      }).catch(e => {
        console.error(e);
      });
    }).catch(e => {
      console.error(e);
      setTimeout(() => that.initiateO10Hub(that), 1000);
    });
  }
}
