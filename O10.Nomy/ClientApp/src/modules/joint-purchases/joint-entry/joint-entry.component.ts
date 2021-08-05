import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { JointPurchasesService } from '../joint-purchases.service';

@Component({
  selector: 'app-joint-entry',
  templateUrl: './joint-entry.component.html',
  styleUrls: ['./joint-entry.component.css']
})
export class JointEntryComponent implements OnInit {

  public isLoaded = false
  public isQrLoaded = false
  public loginQrCode: string | null
  public accountId: number

  constructor(private route: ActivatedRoute, private serviceAccessor: JointPurchasesService) { }

  ngOnInit(): void {
    var that = this

    this.serviceAccessor.getJointServiceAccount().subscribe(
      r => {
        that.accountId = r.accountId
        that.serviceAccessor.getQrCode(that.accountId).subscribe(
          r => {
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
  }

}
