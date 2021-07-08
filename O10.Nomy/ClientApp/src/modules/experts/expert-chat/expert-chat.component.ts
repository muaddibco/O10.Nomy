import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr'
import { ExpertsAccessService } from '../experts-access.service';
import { ExpertProfile } from '../models/expert-profile';
import { PaymentEntry } from '../models/payment-entry';
import { SessionInfo } from '../models/session-info';

@Component({
  selector: 'app-expert-chat',
  templateUrl: './expert-chat.component.html',
  styleUrls: ['./expert-chat.component.css']
})
export class ExpertChatComponent implements OnInit {

  private paymentsHub: HubConnection;
  private chatHub: HubConnection;
  private expertProfile: ExpertProfile;
  public userId: number;
  public sessionInfo: SessionInfo
  public initiatingChat = false
  public isChatConfirmed = false
  public isLoaded = false
  public invoices: PaymentEntry[] = []

  constructor(
    private expertsAccessService: ExpertsAccessService,
    private route: ActivatedRoute) { }

  ngOnInit(): void {
    var expertId = Number(this.route.snapshot.paramMap.get('id'))
    this.userId = Number(this.route.snapshot.paramMap.get('userId'))

    this.paymentsHub = new HubConnectionBuilder()
      .withUrl('/payments')
      .withAutomaticReconnect()
      .build()

    this.paymentsHub.start();

    this.chatHub = new HubConnectionBuilder()
      .withUrl('/chat')
      .withAutomaticReconnect()
      .build()

    this.chatHub.start();

    this.paymentsHub.on("Invoice", i => {
      var invoice = i as PaymentEntry
      this.invoices.push(invoice)
      this.expertsAccessService.payInvoice(this.userId, invoice.sessionId, invoice.commitment, invoice.currency, invoice.amount)
    })

    this.chatHub.on("Confirmed", s => {
      var sessionInfo = s as SessionInfo
      this.isChatConfirmed = true
      this.paymentsHub.invoke("AddToGroup", sessionInfo.sessionId + "_payer");
      this.expertsAccessService.startChat(this.expertProfile.expertProfileId, this.sessionInfo.sessionId).subscribe(
        r => {

        },
        e => {

        })
    })

    var that = this
    this.expertsAccessService.getExpert(expertId).subscribe(
      r => {
        this.isLoaded = true
        this.expertProfile = r
        this.expertsAccessService.initiateChatSession().subscribe(
          s => {
            that.sessionInfo = s
            that.chatHub.invoke("AddToGroup", s.sessionId);
            that.initiatingChat = true
            that.expertsAccessService.inviteToChat(that.expertProfile.expertProfileId, s.sessionId).subscribe(
              a => {

              },
              e => {
                console.error(e);
              }
            )
          },
          e => {
            console.error(e);
          }
        )
      },
      e => {
        console.error(e);
      }
    )

  }

}
