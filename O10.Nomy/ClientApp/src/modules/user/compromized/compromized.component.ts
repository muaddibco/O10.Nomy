import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { AppStateService } from '../../../app/app-state.service';
import { AccountsAccessService } from '../../accounts/accounts-access.service';
import { PasswordConfirmDialog } from '../../password-confirm/password-confirm/password-confirm.dialog';

@Component({
  selector: 'app-compromized',
  templateUrl: './compromized.component.html',
  styleUrls: ['./compromized.component.css']
})
export class CompromizedComponent implements OnInit {

  public isLoaded = false
  private userId: number

  constructor(
    private appState: AppStateService,
    private router: Router,
    private route: ActivatedRoute,
    public dialog: MatDialog,
    private accountsAccessService: AccountsAccessService) { }

  ngOnInit(): void {
    this.appState.setIsMobile(true)
    this.userId = Number(this.route.snapshot.paramMap.get('userId'))

    this.isLoaded = true
  }

  onReset() {
    let that = this;
    var dialogRef = this.dialog.open(PasswordConfirmDialog, { data: { title: "Confirm account reset", confirmButtonText: "Confirm Reset" } });
    dialogRef.afterClosed().subscribe(r => {
      if (r) {
        that.accountsAccessService.reset(this.userId, r).subscribe(
          r1 => {
            console.info('Reset of account passed successfully');
            this.router.navigate(['user-details', this.userId])
          },
          e => {
            console.error('Failed to reset compromised account', e);
          });
      }
    });
  }
}
