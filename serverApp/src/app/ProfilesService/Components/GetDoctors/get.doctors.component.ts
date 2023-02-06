import {Component, OnInit,ViewChild} from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl,ReactiveFormsModule } from '@angular/forms';
import {ActivatedRoute,Router} from "@angular/router";
import { Doctor } from '../../_models/Doctor';
import { Pagination } from '../../_models/Pagination';
import {DomSanitizer} from '@angular/platform-browser';
import { DoctorProfilesService } from '../../_services/doctorProfile.service';
import { GetOfficeResponse } from '../../../OfficesService/_models/responses/GetOfficeResponse';
import { OfficesService } from '../../../OfficesService/_services/office.service';


@Component({
    selector: 'doctors-get',
    providers: [],
    templateUrl: './get.doctors.component.html',
    styleUrls: ['./get.doctors.component.css']
  })
  export class GetDoctorsComponent implements OnInit {

    doctors!:Doctor[];
    offices!:GetOfficeResponse[];
    error = '';
    stop = false;
    submitted = false;
    loading = false;
    pagination: Pagination = new Pagination(1,0,5);
    searchAndFilter!:FormGroup;
    //firstName!:string | number ;
    //lastName!:string | null;
  
    constructor(
      private formBuilder: FormBuilder,
      private route: ActivatedRoute,
      private router: Router,
      private doctorService: DoctorProfilesService,
      private officeService: OfficesService,
      private fb:FormBuilder,
      private sanitizer: DomSanitizer
    )
    {
    }
  
    ngOnInit(): void {
     this.searchAndFilter = new FormGroup({
        "search": new FormControl(null),
        "office": new FormControl(null),
        "firstName": new FormControl(null),
        "lastName": new FormControl(null)
     },)
     this.officeService.getOffices().subscribe((data)=>{
        this.offices = data;
     })

     this.showApiData()
    }


    showApiData(){
        let firstAndLastName = this.searchAndFilter.get("search")?.value;
        if(firstAndLastName != null){
            firstAndLastName = firstAndLastName.split(' ',2);
            this.searchAndFilter.get('firstName')?.setValue(firstAndLastName[0]);
            if(firstAndLastName.length == 2){
              this.searchAndFilter.get('lastName')?.setValue(firstAndLastName[1]);
            }
        }

        this.doctorService.getAll(this.pagination.page,this.pagination.pageSize,this.searchAndFilter.get("firstName")?.value,this.searchAndFilter.get("lastName")?.value,this.searchAndFilter.get("office")?.value?.id)
        .subscribe((data:any) =>
            {
              this.pagination.collectionSize = data.count;
              this.doctors = data.items;

              this.doctors.forEach(doctor => {
                if(doctor.photo != null){
                  let objectURL = 'data:image/png;base64,' + doctor.photo;
                  doctor.photo = this.sanitizer.bypassSecurityTrustUrl(objectURL);
                }
              });
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

    goToDoctor(id:string){

    }

    editDoctor(id:string){
      let currentRole = JSON.parse(localStorage.getItem('currentUser')!).Role.toLowerCase();
      this.router.navigate([`${currentRole}/menu/doctor`,id]);
    }

    createDoctor(){
      let currentRole = JSON.parse(localStorage.getItem('currentUser')!).Role.toLowerCase();
      this.router.navigate([`${currentRole}/menu/doctor/create`]);
    }
  }
