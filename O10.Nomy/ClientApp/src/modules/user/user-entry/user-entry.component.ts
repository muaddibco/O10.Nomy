import { Component, OnInit } from '@angular/core';
import { UserDto } from '../../accounts/models/account'
import { Router } from '@angular/router';
import { AppStateService } from '../../../app/app-state.service';

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
    private appState: AppStateService)
  { }

  ngOnInit(): void {
    this.appState.setIsMobile(true)
    this.isLoaded = true
  }

  onSignIn() {
    this.router.navigate(['user-signin'])
  }

  onSignUp() {
    this.router.navigate(['user-register'])
  }
}
