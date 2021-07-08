import { Component, OnInit } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { Account } from '../../accounts/models/account'
import { Router } from '@angular/router';
import { AccountsAccessService } from '../../accounts/accounts-access.service';

@Component({
  selector: 'app-user-entry',
  templateUrl: './user-entry.component.html',
  styleUrls: ['./user-entry.component.css']
})
export class UserEntryComponent implements OnInit {

  public showUserNotExist = false
  public userAccount: Account
  public isLoaded = false

  constructor(
    private cookieService: CookieService,
    private router: Router,
    private userAccessService: AccountsAccessService)
  { }

  ngOnInit(): void {
    var currentUserId = this.cookieService.get("currentUserId")
    if (currentUserId) {
      this.userAccessService.getAccountById(Number(currentUserId)).subscribe(a => {
        this.userAccount = a
        if (!this.userAccount) {
          this.showUserNotExist = true
          this.isLoaded = true
        } else {
          this.cookieService.set("currentUserId", this.userAccount.accountId.toString())
          sessionStorage.setItem("user", JSON.stringify(this.userAccount))
          this.router.navigate(['user-details'])
        }
      });
    } else {
      this.isLoaded = true
    }
  }

  onSignIn() {
    this.router.navigate(['user-signin'])
  }

  onSignUp() {
    this.router.navigate(['user-register'])
  }
}
