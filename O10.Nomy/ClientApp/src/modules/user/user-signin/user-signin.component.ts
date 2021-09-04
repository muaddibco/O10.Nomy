import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { AccountsAccessService } from '../../accounts/accounts-access.service';

@Component({
  selector: 'app-user-signin',
  templateUrl: './user-signin.component.html',
  styleUrls: ['./user-signin.component.css']
})
export class UserSigninComponent implements OnInit {

  public showError = false
  public isLoaded = false
  public accountNameForm: FormGroup
  public submitted = false;
  public submitClick = false;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private userAccessService: AccountsAccessService) { }

  ngOnInit(): void {
    this.accountNameForm = this.formBuilder.group({
      accountEmail: ['']
    });
    this.isLoaded = true
}

  get formData() { return this.accountNameForm.controls; }

  onSignIn() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.accountNameForm.invalid) {
      return;
    }

    this.submitClick = true;
    this.showError = false

    this.userAccessService.find(this.formData.accountEmail.value).subscribe(
      a => {
        if (a == null) {
          this.showError = true
        } else {
          this.router.navigate(['user-details', a.accountId])
        }
      }
    )
  }

  onCancel() {
    this.router.navigate(['user-entry'])
  }
}
