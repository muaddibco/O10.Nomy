import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AppStateService } from '../../../app/app-state.service';
import { AccountsAccessService } from '../../accounts/accounts-access.service';
import { UserDto } from '../../accounts/models/account';
import { DisclosedSecrets } from '../../accounts/models/disclosed-secrets';

@Component({
  selector: 'app-duplicate-account',
  templateUrl: './duplicate-account.component.html',
  styleUrls: ['./duplicate-account.component.css']
})
export class DuplicateAccountComponent implements OnInit {

  public user: UserDto
  private disclosedSecrets: DisclosedSecrets | null
  public isLoaded = false
  public overrideForm: FormGroup;
  submitted = false;
  submitClick = false;


  constructor(
    private accountAccessService: AccountsAccessService,
    private appState: AppStateService,
    private router: Router,
    private route: ActivatedRoute,
    private formBuilder: FormBuilder) { }

  ngOnInit(): void {
    this.appState.setIsMobile(true)
    var userId = Number(this.route.snapshot.paramMap.get('userId'))
    this.disclosedSecrets = JSON.parse(atob(this.route.snapshot.queryParams['actionInfo']))
    let that = this;
    this.accountAccessService.getAccountById(userId).subscribe(
      r => {
        that.user = r
        that.isLoaded = true
      }
    )

    this.overrideForm = this.formBuilder.group({
      password: ['', Validators.required]
    });
  }

  get formData() { return this.overrideForm.controls; }

  onSubmitOverriding() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.overrideForm.invalid) {
      return;
    }

    this.submitClick = true;
    this.disclosedSecrets.password = this.formData.password.value
    this.accountAccessService.override(this.user.accountId, this.disclosedSecrets).subscribe(r => {
      sessionStorage.removeItem("passwordSet")
      this.router.navigate(['/user-details', this.user.accountId]);
    });
  }

  onCancel() {
    this.router.navigate(['/user-details', this.user.accountId]);
  }
}
