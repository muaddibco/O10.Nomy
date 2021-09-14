import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HubConnection, HubConnectionBuilder, LogLevel } from '@microsoft/signalr';
import { JointGroup, JointPurchasesService } from '../joint-purchases.service';
import { MatDialog } from '@angular/material/dialog';
import { AddJointGroupDialog } from '../add-jointgroup-dialog/add-jointgroup.dialog';

@Component({
  selector: 'app-joint-main',
  templateUrl: './joint-main.component.html',
  styleUrls: ['./joint-main.component.css']
})
export class JointMainComponent implements OnInit {

  public isLoaded = false
  public registrationId: number
  public sessionKey: string | null
  private o10Hub: HubConnection
  public groups: JointGroup[] = []

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private serviceAccessor: JointPurchasesService,
    public dialog: MatDialog) { }

  ngOnInit(): void {
    this.registrationId = Number(this.route.snapshot.paramMap.get('registrationId'))
    this.sessionKey = this.route.snapshot.paramMap.get('sessionKey')

    var that = this

    this.serviceAccessor.getJointGroups(this.registrationId).subscribe(
      r => {
        that.groups = r
      }
    )

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

        that.o10Hub.on("PushAuthorizationCompromised", () => {
          that.router.navigate(['unauthorized-use', that.registrationId, that.sessionKey])
        })

        that.initiateO10Hub(that)
        that.isLoaded = true
      }
    )
  }

  private initiateO10Hub(that: this) {
    this.o10Hub.start().then(() => {
      console.info("Connected to o10Hub");
      this.o10Hub.invoke("AddToGroup", that.sessionKey).then(() => {
        console.info("Added to o10Hub group " + that.sessionKey + " for connection " + that.o10Hub.connectionId);
      }).catch(e => {
        console.error(e);
      });
    }).catch(e => {
      console.error(e);
      setTimeout(() => that.initiateO10Hub(that), 1000);
    });
  }

  goToGroup(group: JointGroup) {
    this.router.navigate(['joint-group-admin', group.jointGroupId, group.o10RegistrationId, this.sessionKey])
  }

  addNewGroup() {
    var dialogRef = this.dialog.open(AddJointGroupDialog);
    dialogRef.afterClosed().subscribe(
      r => {
        if (r) {
          this.serviceAccessor.addJointGroup(this.registrationId, r.name, r.description).subscribe(
            r => {
              this.groups.push(r)
            },
            e => {
              console.error("Failed to add a joint groups with name " + r.name + " and description " + r.description, e);
            }
          )
        }
      }
    )
  }
}
