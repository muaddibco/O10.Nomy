import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { JointGroupMember, JointPurchasesService } from '../joint-purchases.service';
import { AddJointGroupMemberDialog } from '../add-jointgroupmember-dialog/add-jointgroupmember.dialog'
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-joint-group-admin',
  templateUrl: './joint-group-admin.component.html',
  styleUrls: ['./joint-group-admin.component.css']
})
export class JointGroupAdminComponent implements OnInit {

  public isLoaded = false
  public groupId: number
  public registrationId: number
  public sessionKey: string | null
  private o10Hub: HubConnection
  public groupMembers: JointGroupMember[] = []

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private serviceAccessor: JointPurchasesService,
    public dialog: MatDialog) { }

  ngOnInit(): void {
    this.groupId = Number(this.route.snapshot.paramMap.get('groupId'))
    this.registrationId = Number(this.route.snapshot.paramMap.get('registrationId'))
    this.sessionKey = this.route.snapshot.paramMap.get('sessionKey')

    var that = this

    this.serviceAccessor.getJointGroupMembers(this.groupId).subscribe(
      r => {
        that.groupMembers = r
      }
    )

    this.serviceAccessor.getO10HubUri().subscribe(
      r => {
        console.info("Connecting to O10 Hub with URI " + r["o10HubUri"])

        that.o10Hub = new HubConnectionBuilder()
          .withUrl(r["o10HubUri"])
          .build()

        that.initiateO10Hub(that)
        that.isLoaded = true
      }
    )
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

  addNewMember() {
    var dialogRef = this.dialog.open(AddJointGroupMemberDialog);
    dialogRef.afterClosed().subscribe(
      r => {
        if (r) {
          this.serviceAccessor.addJointGroupMember(this.groupId, r.email, r.description).subscribe(
            r => {
              this.groupMembers.push(r)
            },
            e => {
              console.error("Failed to add a joint groups member with email " + r.email + " and description " + r.description, e);
            }
          )
        }
      }
    )
  }
}
