import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { UserAccessService } from '../user-access.service';

@Component({
  selector: 'app-user-registration',
  templateUrl: './user-registration.component.html',
  styleUrls: ['./user-registration.component.css']
})
export class UserRegistrationComponent implements OnInit {

  public isLoaded = false
  public newUserForm: FormGroup
  public submitted = false;
  public submitClick = false;

  constructor(
    private cookieService: CookieService,
    private formBuilder: FormBuilder,
    private userAccessService: UserAccessService,
    private router: Router) { }

  ngOnInit(): void {
    this.newUserForm = this.formBuilder.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', Validators.required, Validators.email],
      password: ['', Validators.required]
    })

    this.isLoaded = true
  }

  get formData() { return this.newUserForm.controls; }


  onSubmit() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.newUserForm.invalid) {
      return;
    }

    this.submitClick = true;

    this.userAccessService.register({
      firstName: this.formData.firstName.value,
      lastName: this.formData.lastName.value,
      email: this.formData.email.value,
      password: this.formData.password.value
    }).subscribe(
      a => {
        this.cookieService.set("currentUserId", a.accountId.toString())
        this.router.navigate(['user-details'])
      },
      e => {
      });
  }

  onCancel() {
    this.router.navigate([''])
  }
}
