import {Component, OnInit,ViewChild} from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl,ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { AuthService } from 'src/app/_services/auth.service';
import {ActivatedRoute,Router} from "@angular/router";
import * as internal from 'stream';
import { PatientProfilesService } from '../../_services/patientProfile.service';
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
    imagePreview!: string;
    submitted = false;
    loading = false;
    maxDate:Date;
    minDate = new Date('1920-04-26')

    constructor(
      private formBuilder: FormBuilder,
      private route: ActivatedRoute,
      private router: Router,
      private profileService: PatientProfilesService,
      private accountService: AccountsService
    )
    {
      let now = new Date();
      now.setFullYear(now.getFullYear() - 18);
      this.maxDate = now;
    }

    ngOnInit(): void {
      this.accountId = this.route.snapshot.paramMap.get("accountId")!;
      this.accountService.checkPatientAccountBeforeCreation(this.accountId).subscribe(
        data=>{},
        error => {
          console.log(error.status)
          this.error = 'Sorry but url is incorrect';
          this.stop = true;
        }
      )

      this.profileForm = new FormGroup({
        "firstName": new FormControl("", [Validators.required]),
        "lastName": new FormControl("", [Validators.required]),
        "middleName": new FormControl(null),
        "phoneNumber": new FormControl("+",[Validators.pattern('^\\+[0-9][0-9]*')]),
        "dateOfBirth": new FormControl("", [Validators.required]),
        "accountId": new FormControl(this.accountId),
        "photo": new FormControl(null),

      });
    }

    get f() { return this.profileForm.controls; }

    onSubmit() {
      this.submitted = true;

      if (this.profileForm.invalid) {
        return;
      }
      const time =(new Date(this.profileForm.get('dateOfBirth')?.value)).toUTCString();

      const formData = new FormData();
      formData.append("firstName",this.profileForm.get('firstName')?.value)
      formData.append("lastName",this.profileForm.get('lastName')?.value)
      formData.append("middleName",this.profileForm.get('middleName')?.value)
      formData.append("phoneNumber",this.profileForm.get('phoneNumber')?.value)
      formData.append("dateOfBirth",time)
      formData.append("accountId",this.profileForm.get('accountId')?.value)
      formData.append("photo",this.profileForm.get('photo')?.value)
      this.loading = true;
      console.log(typeof(this.profileForm.get('dateOfBirth')?.value))
      this.profileService.createProfile(formData)
        .subscribe(
          data => {
            //this.photoData = new FormData();
          },
          error => {
            this.loading = false;
            console.log(error.status)
            this.error = 'Either an email or a password is incorrect';
          });
          this.router.navigate(['login']);
    }

    upload(files:any) {
      if (files.length === 0)
        return;
      //this.photoData = new FormData();

      for (const file of files) {
        this.profileForm.get('photo')?.setValue(file);
        //this.photoData.append(file.name, file);
      }
    }

    onSelect(files:any) {
      if (files.length === 0)
        return;

      const file = files[0];
      this.profileForm.patchValue({photo:file});
      this.profileForm.get('photo')?.updateValueAndValidity();
      const reader = new FileReader();
      reader.onload = () => {
        this.imagePreview = reader.result?.toString()!;
      };
      reader.readAsDataURL(file);

    }


  }
