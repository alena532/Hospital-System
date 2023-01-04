import {Component, OnInit,ViewChild} from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl,ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { AuthService } from 'src/app/_services/auth.service';
import {ActivatedRoute,Router} from "@angular/router";
import * as internal from 'stream';
import { AccountsService } from '../../_services/account.service';
import { DoctorProfilesService } from '../../_services/doctorProfile.service';
import { Doctor } from '../../_models/Doctor';
import { NgbPaginationModule } from '@ng-bootstrap/ng-bootstrap';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { Pagination } from '../../_models/Pagination';
import { Observable } from 'rxjs';
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
    
  
    constructor(
      private formBuilder: FormBuilder,
      private route: ActivatedRoute,
      private router: Router,
      private doctorService: DoctorProfilesService,
      private accountService: AccountsService
    )
    {

    }
  
    ngOnInit(): void {
     this.showApiData()
    }


    showApiData(){
        this.doctorService.getAll(this.pagination.page,this.pagination.pageSize).subscribe((data:any) =>
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
  }
