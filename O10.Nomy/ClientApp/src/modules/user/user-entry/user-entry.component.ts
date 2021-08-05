import { Component, OnInit } from '@angular/core';
import { UserDto } from '../../accounts/models/account'
import { Router } from '@angular/router';
import { AccountsAccessService } from '../../accounts/accounts-access.service';

@Component({
  selector: 'app-user-entry',
  templateUrl: './user-entry.component.html',
  styleUrls: ['./user-entry.component.css']
})
export class UserEntryComponent implements OnInit {

  public showUserNotExist = false
  public userAccount: UserDto
  public isLoaded = false

  constructor(
    private router: Router,
    private userAccessService: AccountsAccessService)
  { }

  ngOnInit(): void {
    this.isLoaded = true
  }

  onSignIn() {
    this.router.navigate(['user-signin'])
  }

  onSignUp() {
    this.router.navigate(['user-register'])
  }
}
