import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Account } from '../../accounts/models/account';
import { UserAttributeScheme } from '../models/user-attribute-scheme';
import { UserAccessService } from '../user-access.service';
import { PasswordConfirmDialog } from '../../password-confirm/password-confirm/password-confirm.dialog'
import { Router } from '@angular/router';
import { HubConnectionBuilder, HubConnection } from '@microsoft/signalr'
import { SessionExpertInfo } from '../models/session-expert-info';
import { ExpertsAccessService } from '../../experts/experts-access.service';
import { ExpertProfile } from '../../experts/models/expert-profile';
import { Subscription } from 'rxjs';
import { interval } from 'rxjs';

@Component({
  selector: 'app-user-details',
  templateUrl: './user-details.component.html',
  styleUrls: ['./user-details.component.css'],
  encapsulation: ViewEncapsulation.None
})
export class UserDetailsComponent implements OnInit {

  public user: Account
  public nomyIdentity: UserAttributeScheme
  public isLoaded = false
  private chatHub: HubConnection
  private paymentHub: HubConnection
  public isInSession = false
  public isSessionStarted = false
  public sessionInfo: SessionExpertInfo
  public expertProfile: ExpertProfile
  private paymentSubscription: Subscription

  constructor(
    private userAccessService: UserAccessService,
    private expertAccessService: ExpertsAccessService,
    private router: Router,
    public dialog: MatDialog ) { }

  ngOnInit(): void {
    this.user = JSON.parse(sessionStorage.getItem("user"))
    let that = this;
    var dialogRef = this.dialog.open(PasswordConfirmDialog, { data: { title: "Start account", confirmButtonText: "Submit" } });
    dialogRef.afterClosed().subscribe(
      r => {
        if (r) {

        }

        that.isLoaded = true
      }
    )

    this.userAccessService.getUserAttributes(this.user).subscribe(
      r => {
        if (r && r.length > 0) {
          this.nomyIdentity = r[0]
        }
      },
      e => {

      }
    )

    this.paymentHub = new HubConnectionBuilder()
      .withUrl('/payments')
      .withAutomaticReconnect()
      .build()

    this.paymentHub.start()

    this.chatHub = new HubConnectionBuilder()
      .withUrl('/chat')
      .build()

    this.initiateChatHub(this);


    this.chatHub.on("Invitation", r => {
      this.sessionInfo = r as SessionExpertInfo
      if (confirm("There is an invitation for a session with id " + this.sessionInfo.sessionId)) {
        this.isInSession = true
        this.expertAccessService.getExpert(this.sessionInfo.expertProfileId).subscribe(
          r => {
            this.expertProfile = r
          }
        )

        this.paymentHub.invoke("AddToGroup", this.sessionInfo.sessionId + "_Payee");

        this.userAccessService.confirmSession(this.sessionInfo.sessionId).subscribe(
          r => {

          },
          e => {

          })
      }
    })

    this.chatHub.on("Start", r => {
      this.isSessionStarted = true
      this.paymentSubscription = interval(30000).subscribe(v => {

      })
    })
  }

    private initiateChatHub(that: this) {
        this.chatHub.start().then(() => {
            console.info("Connected to chatHub");
            this.chatHub.invoke("AddToGroup", that.user.accountId.toString()).then(() => {
              console.info("Added to chatHub group " + that.user.accountId.toString());
            }).catch(e => {
                console.error(e);
            });
        }).catch(e => {
          console.error(e);
          setTimeout(() => that.initiateChatHub(that), 1000);
        });
    }

  ngOnDestroy() {
    if (this.paymentSubscription) {
      this.paymentSubscription.unsubscribe();
    }
  }

  gotoExperts() {
    this.router.navigate(['experts-list', this.user.accountId])
  }
}
