import {Component, OnInit,ViewChild} from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl,ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { AuthService } from 'src/app/_services/auth.service';
import {ActivatedRoute,Router} from "@angular/router";
import * as internal from 'stream';
import { AccountsService } from '../../_services/account.service';
import { DoctorProfilesService } from '../../_services/doctorProfile.service';
import { PatientProfilesService } from '../../_services/patientProfile.service';
import { Doctor } from '../../_models/Doctor';
import { NgbPaginationModule } from '@ng-bootstrap/ng-bootstrap';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { Pagination } from '../../_models/Pagination';
import { Observable } from 'rxjs';
import {Office} from './../../_models/Office'
@Component({
    selector: 'doctors-get',
    providers: [],
    templateUrl: './get.doctors.component.html',
    styleUrls: ['./get.doctors.component.css']
  })
  export class GetDoctorsComponent implements OnInit {

    doctors!:Doctor[] 
    error = '';
    stop = false;
    submitted = false;
    loading = false;
    pagination: Pagination = new Pagination(1,0,5);
    searchAndFilter!:FormGroup;
    offices!:Office[];
  
    constructor(
      private formBuilder: FormBuilder,
      private route: ActivatedRoute,
      private router: Router,
      private doctorService: DoctorProfilesService,
      private patientService: PatientProfilesService,
      private accountService: AccountsService,
      private fb:FormBuilder
    )
    {

    }
  
    ngOnInit(): void {
     this.searchAndFilter = new FormGroup({
        "search": new FormControl(null),
        "office": new FormControl(null)
     },)
     this.patientService.getOffices().subscribe((data)=>{
        this.offices = data;
     })

     this.showApiData()
    }


    showApiData(){
        let firstName = null;
        let lastName = null;
        let firstAndLastName =this.searchAndFilter.get("search")?.value;
        if(firstAndLastName!= null){
            firstAndLastName = firstAndLastName.split(' ',2);
            firstName = firstAndLastName[0];
            lastName = null;
            if(firstAndLastName.length == 2){
                lastName = firstAndLastName[1];
            }
        }

        this.doctorService.getAll(this.pagination.page,this.pagination.pageSize,firstName,lastName,this.searchAndFilter.get("office")?.value).subscribe((data:any) =>
            {
                this.pagination.collectionSize = data.count;
                this.doctors = data.items;
            }
        )
    }

    onPageChange(event:any){
        this.pagination.page = event;
        this.showApiData();
    }

      onSubmit() {
        this.submitted = true;
  
        if (this.searchAndFilter.invalid) {
          return;
        }
        this.loading = true;
        this.showApiData();
    }
      
  }
