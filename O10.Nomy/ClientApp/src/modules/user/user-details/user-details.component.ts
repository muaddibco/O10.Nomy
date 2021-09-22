import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { UserDto } from '../../accounts/models/account';
import { UserAccessService } from '../user-access.service';
import { PasswordConfirmDialog } from '../../password-confirm/password-confirm/password-confirm.dialog'
import { ActivatedRoute, Router } from '@angular/router';
import { HubConnectionBuilder, HubConnection, LogLevel } from '@microsoft/signalr'
import { MatBottomSheet, MatBottomSheetRef } from '@angular/material/bottom-sheet';
import { QrCodePopupComponent } from '../../qrcode-popup/qrcode-popup/qrcode-popup.component';
import { SessionExpertInfo } from '../models/session-expert-info';
import { ExpertsAccessService } from '../../experts/experts-access.service';
import { ExpertProfile } from '../../experts/models/expert-profile';
import { Subscription } from 'rxjs';
import { DataSource } from '@angular/cdk/collections';
import { interval } from 'rxjs';
import { AccountsAccessService } from '../../accounts/accounts-access.service';
import { animate, state, style, transition, trigger } from '@angular/animations';
import { PaymentRecordEntry } from '../../experts/models/payment-record-entry';
import { ReplaySubject } from 'rxjs';
import { Observable } from 'rxjs';
import { AppStateService } from '../../../app/app-state.service';
import { UnauthorizedUse } from '../models/unauthorized-use';

@Component({
  selector: 'app-user-details',
  templateUrl: './user-details.component.html',
  styleUrls: ['./user-details.component.css'],
  encapsulation: ViewEncapsulation.None,
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({ height: '0px', minHeight: '0' })),
      state('expanded', style({ height: '*' })),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ]
})
export class UserDetailsComponent implements OnInit {

  public user: UserDto
  public isLoaded = false
  private chatHub: HubConnection
  private paymentHub: HubConnection
  private o10Hub: HubConnection
  public isInSession = false
  public isSessionStarted = false
  public sessionInfo: SessionExpertInfo
  public expertProfile: ExpertProfile
  private paymentSubscription: Subscription
  public payments: PaymentRecordEntry[] = []
  public dataSource = new PaymentsDataSource(this.payments)

  public displayedColumns: string[] = ['commitment']
  public expandedElement: PaymentRecordEntry | null
  private qrCodeSheetRef: MatBottomSheetRef<QrCodePopupComponent> = null;

  constructor(
    private userAccessService: UserAccessService,
    private accountAccessService: AccountsAccessService,
    private expertAccessService: ExpertsAccessService,
    private appState: AppStateService,
    private router: Router,
    private route: ActivatedRoute,
    public dialog: MatDialog) { }

  ngOnInit(): void {
    this.appState.setIsMobile(true)
    var userId = Number(this.route.snapshot.paramMap.get('userId'))

    let that = this;
    this.accountAccessService.getAccountById(userId).subscribe(
      async r => {
        that.user = r

        var passwordSet = false
        if (sessionStorage.getItem("passwordSet") === "true") {
          var res = await that.accountAccessService.isAuthenticated(r.accountId).toPromise()
          passwordSet = res.isAuthenticated
        }

        if (!passwordSet) {
          var dialogRef = that.dialog.open(PasswordConfirmDialog, { data: { title: "Start account", confirmButtonText: "Submit" } });
          dialogRef.afterClosed().subscribe(
            r => {
              if (r) {
                that.authenticateUser(that, r);
              }

              that.userAccessService.getUserAccountDetails(that.user.accountId).subscribe(
                r => {
                  if (r.isCompromised) {
                    that.router.navigate(['/compromized', that.user.accountId])
                  }
                },
                e => {}
              )

              that.isLoaded = true
            }
          )
        } else {
          that.isLoaded = true

          that.userAccessService.getUserAccountDetails(that.user.accountId).subscribe(
            r => {
              if (r.isCompromised) {
                that.router.navigate(['/compromized', that.user.accountId])
              }
            },
            e => { }
          )

          that.initiateChatHub(that);
        }

        that.userAccessService.getO10HubUri().subscribe(
          r => {
            console.info("Connecting to O10 Hub with URI " + r["o10HubUri"])

            that.o10Hub = new HubConnectionBuilder()
              .withAutomaticReconnect()
              .configureLogging(LogLevel.Debug)
              .withUrl(r["o10HubUri"])
              .build()

            that.o10Hub.onreconnected(c => {
              this.o10Hub.invoke("AddToGroup", that.user.o10Id.toString()).then(() => {
                console.info("Added to o10Hub group " + that.user.o10Id.toString() + " for connection " + that.o10Hub.connectionId);
              }).catch(e => {
                console.error(e);
              });
            });

            that.o10Hub.on("PushUnauthorizedUse", r => {
              console.info("Handled PushUnauthorizedUse: " + JSON.stringify(r))
              that.userAccessService.sendCompromizationClaim(that.user.accountId, r as UnauthorizedUse).subscribe(
                r => {
                  that.router.navigate(['compromized', that.user.accountId])
                },
                e => {
                  console.error("Failed to send compromization claim", e)
                }
              )
            })

            that.initiateO10Hub(that)
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

    this.paymentHub.on("Payment", p => {
      var paymentEntry = p as PaymentRecordEntry
      console.info("Obtained payment " + paymentEntry.commitment)
      that.payments.push(paymentEntry)
      that.dataSource.setData(that.payments)
    })

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
      this.issueInvoice();
      this.paymentSubscription = interval(60000).subscribe(v => {
        this.issueInvoice();
      })
    })
  }

  private issueInvoice() {
      console.info("Issue invoice for " + this.expertProfile.fee + " USD");
      this.userAccessService.sendInvoice(this.user.accountId, this.sessionInfo.sessionId, this.expertProfile.fee, "USD").subscribe(
          r => {
              console.info("Invoice " + r.commitment + " for the session " + this.sessionInfo.sessionId + " issued successfully");
          },
          e => {
              console.error("Failed to issue an invoice for the session " + this.sessionInfo.sessionId, e);
          });
  }

  private authenticateUser(that: this, password: string) {
    that.accountAccessService.authenticate(that.user.accountId, password)
      .subscribe(
        a => {
          console.info("user authenticated successfully")
          sessionStorage.setItem("passwordSet", "true")
          that.initiateChatHub(that);
        },
        e => {
          console.error("failed to authenticate user", e)
          alert("Failed to authenticate user")
          that.router.navigate(['/'])
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

  onDiscloseSecrets() {
    this.router.navigate(['reveal-secrets', this.user.accountId])
  }

  private initiateO10Hub(that: this) {
    this.o10Hub.start().then(() => {
      console.info("Connected to o10Hub");
      this.o10Hub.invoke("AddToGroup", that.user.o10Id.toString()).then(() => {
        console.info("Added to o10Hub group " + that.user.o10Id);
      }).catch(e => {
        console.error(e);
      });
    }).catch(e => {
      console.error(e);
      setTimeout(() => that.initiateO10Hub(that), 1000);
    });
  }

  gotoExperts() {
    this.router.navigate(['experts-list', this.user.accountId])
  }

  onMyAttributes() {
    this.router.navigate(['user-attributes', this.user.accountId])
  }

  public gotoQrScan() {
    this.router.navigate(['qr-scan', this.user.accountId])
  }

  public gotoDuplicate() {
    this.router.navigate(['duplicate', this.user.accountId])
  }

  logout() {
    sessionStorage.removeItem("passwordSet")
    this.router.navigate(['user-entry'])
  }
}

class PaymentsDataSource extends DataSource<PaymentRecordEntry> {
  private _dataStream = new ReplaySubject<PaymentRecordEntry[]>();

  constructor(initialData: PaymentRecordEntry[]) {
    super();
    this.setData(initialData);
  }

  connect(): Observable<PaymentRecordEntry[]> {
    return this._dataStream;
  }

  disconnect() { }

  setData(data: PaymentRecordEntry[]) {
    this._dataStream.next(data);
  }
}
