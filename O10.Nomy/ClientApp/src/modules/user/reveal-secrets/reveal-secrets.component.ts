import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { AppStateService } from '../../../app/app-state.service';
import { PasswordConfirmDialog } from '../../password-confirm/password-confirm/password-confirm.dialog';
import { UserAccessService } from '../user-access.service';

@Component({
  selector: 'app-reveal-secrets',
  templateUrl: './reveal-secrets.component.html',
  styleUrls: ['./reveal-secrets.component.css']
})
export class RevealSecretsComponent implements OnInit {

  private userId: number
  public isLoaded = false
  public showQr = false
  public qrCode: string | null

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    public dialog: MatDialog,
    private userAccessService: UserAccessService,
    private appState: AppStateService  ) { }

  ngOnInit(): void {
    this.appState.setIsMobile(true)
    this.userId = Number(this.route.snapshot.paramMap.get('userId'))
    let that = this;
    var dialogRef = this.dialog.open(PasswordConfirmDialog, { data: { confirmButtonText: "Confirm Disclose" } });
    dialogRef.afterClosed().subscribe(r => {
      if (r) {
        that.userAccessService.getDisclosedSecrets(this.userId, r).subscribe(
          r1 => {
            that.qrCode = r1.code
            that.showQr = true
            that.isLoaded = true
          },
          e => {
            that.showQr = false
            that.isLoaded = true
            alert('Failed to disclose secrets');
          });
      } else {
        that.showQr = false
        that.isLoaded = true
      }
    });
  }

  cancel() {
    this.router.navigate(['user-details', this.userId])
  }
}
