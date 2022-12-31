import {Component, OnInit} from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl,ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { AuthService } from 'src/app/_services/auth.service';
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
      if (this.loginForm.invalid) {
        return;
      }
  
      this.authService.login(this.f['email'].value, this.f['password'].value)
        .subscribe(
          data => {},
          error => {
            console.log(error.status)
            if(error.status === 400){
              this.error = 'Either an email or a password is incorrect';
            }
          });
    }
  }
  