import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AppStateService } from '../../../app/app-state.service';
import { AccountsAccessService } from '../../accounts/accounts-access.service';
import { UserDto } from '../../accounts/models/account';

@Component({
  selector: 'app-duplicate-account',
  templateUrl: './duplicate-account.component.html',
  styleUrls: ['./duplicate-account.component.css']
})
export class DuplicateAccountComponent implements OnInit {

  public user: UserDto
  public isLoaded = false
  public duplicationForm: FormGroup;
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
    let that = this;
    this.accountAccessService.getAccountById(userId).subscribe(
      r => {
        that.user = r
        that.isLoaded = true
      }
    )

    this.duplicationForm = this.formBuilder.group({
      accountInfo: ['', Validators.required]
    });
  }

  get formData() { return this.duplicationForm.controls; }

  onSubmitDuplicating() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.duplicationForm.invalid) {
      return;
    }

    this.submitClick = true;

    this.accountAccessService.duplicate(this.user.accountId, this.formData.accountInfo.value).subscribe(r => {
      this.router.navigate(['/user-details', this.user.accountId]);
    });
  }

  onCancelDuplicating() {
    this.router.navigate(['/user-details', this.user.accountId]);
  }
}
