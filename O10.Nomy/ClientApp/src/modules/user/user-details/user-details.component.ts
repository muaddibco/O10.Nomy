import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Account } from '../../accounts/models/account';
import { UserAttributeScheme } from '../models/user-attribute-scheme';
import { UserAccessService } from '../user-access.service';
import { PasswordConfirmDialog } from '../../password-confirm/password-confirm/password-confirm.dialog'
import { ActivatedRoute, Router } from '@angular/router';
import { HubConnectionBuilder, HubConnection } from '@microsoft/signalr'
import { SessionExpertInfo } from '../models/session-expert-info';
import { ExpertsAccessService } from '../../experts/experts-access.service';
import { ExpertProfile } from '../../experts/models/expert-profile';
import { Subscription } from 'rxjs';
import { interval } from 'rxjs';
import { AccountsAccessService } from '../../accounts/accounts-access.service';

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
    private accountAccessService: AccountsAccessService,
    private expertAccessService: ExpertsAccessService,
    private router: Router,
    private route: ActivatedRoute,
    public dialog: MatDialog ) { }

  ngOnInit(): void {
    var userId = Number(this.route.snapshot.paramMap.get('userId'))
    let that = this;
    this.accountAccessService.getAccountById(userId).subscribe(
      r => {
        that.user = r
        var dialogRef = that.dialog.open(PasswordConfirmDialog, { data: { title: "Start account", confirmButtonText: "Submit" } });
        dialogRef.afterClosed().subscribe(
          r => {
            if (r) {

              that.initiateChatHub(that);

              that.initiateUserAttributes(that);
            }

            that.isLoaded = true
          }
        )
      },
      e => {
      })

    this.paymentHub = new HubConnectionBuilder()
      .withUrl('/payments')
      .withAutomaticReconnect()
      .build()

    this.paymentHub.start()

    this.chatHub = new HubConnectionBuilder()
      .withUrl('/chat')
      .build()

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
      var sessionInfo = r as SessionExpertInfo
      console.info("Started session " + sessionInfo.sessionId + ", launching periodic invoice issuing...")
      this.isSessionStarted = true
      this.paymentSubscription = interval(30000).subscribe(v => {
        console.info("Issue invoice for " + this.expertProfile.fee + " USD")
        this.userAccessService.sendInvoice(this.user.accountId, this.sessionInfo.sessionId, this.expertProfile.fee, "USD").subscribe(
          r => {
            console.info("Invoice for the session " + this.sessionInfo.sessionId + " issued successfully")
          },
          e => {
            console.error("Failed to issue an invoice for the session " + this.sessionInfo.sessionId, e)
          })
      })
    })
  }

    private initiateUserAttributes(that: this) {
        that.userAccessService.getUserAttributes(that.user.accountId).subscribe(
            r => {
                if (r && r.length > 0) {
                    this.nomyIdentity = r[0];
                }
            },
            e => {
            }
        );
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
