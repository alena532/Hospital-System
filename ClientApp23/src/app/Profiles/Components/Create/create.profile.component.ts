import {Component, OnInit,ViewChild} from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl,ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { DatePickerComponent } from '@syncfusion/ej2-angular-calendars';
import { AuthService } from 'src/app/_services/auth.service';
import {ActivatedRoute,Router} from "@angular/router";
import * as internal from 'stream';
import { ButtonComponent } from '@syncfusion/ej2-angular-buttons';


@Component({
    selector: 'profile-create',
    providers: [],
    templateUrl: './create.profile.component.html',
    styleUrls: ['./create.profile.component.css']
  })
  export class CreateProfileComponent implements OnInit {
    @ViewChild('ejDatePicker') ejDatePicker!: DatePickerComponent;
    profileForm: FormGroup;
    public placeholder: String = 'Date of Birth';
    accountId!: string;
    error = '';

    constructor(
      private formBuilder: FormBuilder,
      private router: ActivatedRoute,
      private authService: AuthService
    )
    {
      this.profileForm = new FormGroup({
        "firstName": new FormControl("", [Validators.required]),
        "lastName": new FormControl("", [Validators.required]),
        "middleName": new FormControl(""),
        "phoneNumber": new FormControl("+",[Validators.pattern('^\\+[0-9]*')]),
        "dateOfBirth": new FormControl("", [Validators.required])

      });
    }

    ngOnInit(): void {
        this.accountId = this.router.snapshot.params['accountId'];
    }

    get f() { return this.profileForm.controls; }

    onSubmit() {
      if (this.profileForm.invalid) {
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
