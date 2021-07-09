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

    var that = this

    this.chatHub.start().then(() => {
      console.info("chatHub started, getting expert info for expertId " + expertId)
      that.expertsAccessService.getExpert(expertId).subscribe(
        r => {
          that.isLoaded = true
          that.expertProfile = r

          console.info("Intiating chat session with the expert " + r.firstName + " " + r.lastName)
          that.expertsAccessService.initiateChatSession().subscribe(
            s => {
              console.info("Chat session with sessionId " + s.sessionId + " initiated")
              that.sessionInfo = s
              that.chatHub.invoke("AddToGroup", s.sessionId);
              that.initiatingChat = true
              console.info("Sending chat invitation to " + that.expertProfile.firstName + " " + that.expertProfile.lastName)
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
    });

    this.paymentsHub.on("Invoice", i => {
      console.info("Received invoice " + JSON.stringify(i))
      var invoice = i as PaymentEntry
      that.invoices.push(invoice)
      console.info("Pay invoice " + invoice.commitment)
      that.expertsAccessService.payInvoice(that.userId, invoice.sessionId, invoice.commitment, "USD", that.expertProfile.fee).subscribe(
        r => {
          console.info("Invoice " + invoice.commitment + " paid with payment " + r.commitment)
        },
        e => {
          console.error("Failed to pay invoice " + invoice.commitment, e)
        })
    })

    this.chatHub.on("Confirmed", s => {
      var sessionInfo = s as SessionInfo
      that.isChatConfirmed = true
      console.info("Confirmation for session " + sessionInfo.sessionId + " from " + that.expertProfile.firstName + " " + that.expertProfile.lastName + " received, adding to group " + sessionInfo.sessionId + "_Payer")
      that.paymentsHub.invoke("AddToGroup", sessionInfo.sessionId + "_Payer").then(
        () => {
          console.info("Added to group " + sessionInfo.sessionId + "_Payer, starting chat...")
          that.expertsAccessService.startChat(that.expertProfile.expertProfileId, that.sessionInfo.sessionId).subscribe(
            r => {

            },
            e => {

            })
        });
    })
  }
}
