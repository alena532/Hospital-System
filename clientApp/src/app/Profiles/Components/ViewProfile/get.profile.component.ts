import {Component, OnInit,ViewChild} from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl,ReactiveFormsModule } from '@angular/forms';
import {ActivatedRoute,Router} from "@angular/router";
import { AccountsService } from '../../_services/account.service';
import { DoctorProfilesService } from '../../_services/doctorProfile.service';
import { PatientProfilesService } from '../../_services/patientProfile.service';
import { Doctor } from '../../_models/Doctor';
import { Pagination } from '../../_models/Pagination';
import {Office} from './../../_models/Office'
import { PhotosService } from 'src/app/Documents/_services/photo.service';
import { AuthService } from 'src/app/_services/auth.service';
import { ConditionalExpr } from '@angular/compiler';
import {DomSanitizer} from '@angular/platform-browser';
import {User} from '../../../_models/User';
@Component({
    selector: 'profile-get',
    providers: [],
    templateUrl: './get.profile.component.html',
    styleUrls: ['./get.profile.component.css']
  })
  export class GetProfileComponent implements OnInit {
    error = '';
    stop = false;
    submitted = false;
    loading = false;
    searchAndFilter!:FormGroup;
    offices!:Office[];
    image!: any;
    currentUser!:User;
  
    constructor(
      private formBuilder: FormBuilder,
      private route: ActivatedRoute,
      private router: Router,
      private doctorService: DoctorProfilesService,
      private patientService: PatientProfilesService,
      private accountService: AccountsService,
      private fb:FormBuilder,
      private photoService: PhotosService,
      public authService: AuthService,
      private sanitizer: DomSanitizer
    )
    {
    }
  
    ngOnInit(): void {
        console.log(this.authService.currentUser);
        this.currentUser = this.authService.getCurrentUser();
        
        this.photoService.getByPatientId(this.currentUser.id).subscribe(
            (data:any)=>{
                let objectURL = 'data:image/png;base64,' + data;
            this.image = this.sanitizer.bypassSecurityTrustUrl(objectURL);
            }
        )
       
        console.log(this.image)
        
    }



   

     
  }
