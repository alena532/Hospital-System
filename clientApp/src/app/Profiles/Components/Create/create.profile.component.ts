import {Component, OnInit,ViewChild} from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl,ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { AuthService } from 'src/app/_services/auth.service';
import {ActivatedRoute,Router} from "@angular/router";
import * as internal from 'stream';
import { ProfilesService } from '../../_services/profiles.service';
import { AccountsService } from '../../_services/account.service';



@Component({
    selector: 'profile-create',
    providers: [],
    templateUrl: './create.profile.component.html',
    styleUrls: ['./create.profile.component.css']
  })
  export class CreateProfileComponent implements OnInit {

    profileForm!: FormGroup;
    public placeholder: String = 'Date of Birth';
    accountId!: string;
    error = '';
    stop = false;
    submitted = false;
    loading = false;
    maxDate:Date;
    minDate = new Date('1920-04-26')
  
    constructor(
      private formBuilder: FormBuilder,
      private router: ActivatedRoute,
      private profileService: ProfilesService,
      private accountService: AccountsService

    )
    {
      let now = new Date();
      now.setFullYear(now.getFullYear() - 18);
      this.maxDate = now;

     
    }
  
    ngOnInit(): void {
      this.profileForm = new FormGroup({
        "firstName": new FormControl("", [Validators.required]),
        "lastName": new FormControl("", [Validators.required]),
        "middleName": new FormControl(null),
        "phoneNumber": new FormControl("+",[Validators.pattern('^\\+[0-9][0-9]*')]),
        "dateOfBirth": new FormControl("", [Validators.required]),
        "photo": new FormControl(null),

      });
      this.accountId = this.router.snapshot.params['accountId'];
      this.accountService.getById(this.accountId).subscribe(
        error => {
          console.log(error.status)
          if(error.status === 400){
            this.error = 'Sorry but url is incorrect';
            this.stop = true;
          }
        }
      )
    }
  
    get f() { return this.profileForm.controls; }
  
    onSubmit() {
      this.submitted = true;

      if (this.profileForm.invalid) {
        return;
      }
      this.loading = true;
      this.profileService.createProfile(this.profileForm.value)
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
  