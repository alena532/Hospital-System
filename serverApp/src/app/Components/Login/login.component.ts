import {Component, OnInit} from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl,ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { AuthService} from '../../_services/auth.service';
import {Router} from "@angular/router";

@Component({
    selector: 'app-login',
    providers: [],
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css']
  })
  export class LoginComponent implements OnInit {
    loginForm: FormGroup;
    error = '';
    submitted = false;
    loading = false;
    userId! :string;
  
    constructor(
      private formBuilder: FormBuilder,
      private router: Router,
      private authService: AuthService
    )
    {
      this.loginForm = new FormGroup({
        "password": new FormControl("", [Validators.required, Validators.minLength(8)]),
        "email": new FormControl("", [ Validators.required, Validators.email]),
      });
    }
  
    ngOnInit(): void {
    }
  
    get f() { return this.loginForm.controls; }
  
    onSubmit() {
      this.submitted = true;

      if (this.loginForm.invalid) {
        return;
      }
      
      this.loading = true;

      this.authService.login(this.f['email'].value, this.f['password'].value)
        .subscribe(
          data => {
            this.loading = false;
            this.router.navigate(['receptionist']);
          },
          error => {
            this.authService.logout();
            this.loading = false;
            console.log(error.status)
            this.error = 'Either an email or a password is incorrect';
          }
        );

    }
  }
  