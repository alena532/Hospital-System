import {Component, OnInit} from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl,ReactiveFormsModule} from '@angular/forms';
import { HttpClientModule, HttpClient, HttpStatusCode } from '@angular/common/http';
import { AuthService} from '../../_services/auth.service'
import {Router} from "@angular/router";

@Component({
    selector: 'app-register',
    providers: [],
    templateUrl: './register.component.html',
    styleUrls: ['./register.component.css']
  })
  export class RegisterComponent implements OnInit {
    registerForm: FormGroup;
    error = '';
    submitted = false;
    loading = false;

    constructor(
      private formBuilder: FormBuilder,
      private router: Router,
      private authService: AuthService
    )
    {
      this.registerForm = new FormGroup({
        "password": new FormControl("", [Validators.required, Validators.minLength(8)]),
        "email": new FormControl("", [ Validators.required, Validators.email]),
        "passwordAgain": new FormControl("", [Validators.required, Validators.minLength(8)]),

      });
    }

    ngOnInit(): void {
    }

    get f() { return this.registerForm.controls; }

    onSubmit() {
      this.submitted = true;

      if (this.registerForm.invalid) {
        return;
      }
      if(this.f['password'].value != this.f['passwordAgain'].value){
        this.error = 'Passwords must match';
        return;
      }

      this.loading = true;

      this.authService.register(this.f['email'].value, this.f['password'].value)
        .subscribe(
          data => {
            this.router.navigate(['login']);
          },
          error => {
            console.log(error.status)
            this.loading = false;
            this.error = 'Someone already uses this email';
          });
    }
  }
