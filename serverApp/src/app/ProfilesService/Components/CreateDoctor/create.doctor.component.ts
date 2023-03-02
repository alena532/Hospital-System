import {Component, OnInit,ViewChild} from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl,ReactiveFormsModule } from '@angular/forms';
import {ActivatedRoute,Router} from "@angular/router";
import { DoctorProfilesService } from '../../_services/doctorProfile.service';
import { GetOfficeResponse } from '../../../OfficesService/_models/responses/GetOfficeResponse';
import { OfficesService } from '../../../OfficesService/_services/office.service';
import { GetSpecializationResponse } from 'src/app/ServicesService/_models/responses/GetSpecializationResponse';
import { GetDoctorProfilesResponse } from '../../_models/responses/GetDoctorProfilesResponse';
import { SpecializationService } from 'src/app/ServicesService/_services/specialization.service';
import { DoctorStatusEnum } from '../../_models/DoctorStatusEnum';
import { MatDatepicker } from '@angular/material/datepicker';

@Component({
    selector: 'doctor-create',
    providers: [],
    templateUrl: './create.doctor.component.html',
    styleUrls: ['./create.doctor.component.css']
  })
  export class CreateDoctorComponent implements OnInit {
    createForm!: FormGroup;
    public placeholder: String = 'Date of Birth';
    error = '';
    stop = false;
    imagePreview!: string;
    submitted = false;
    loading = false;
    maxDate:Date;
    offices!:GetOfficeResponse[];
    specializations!:GetSpecializationResponse[];
    minDate = new Date('1920-04-26')
    doctorStatusEnum = DoctorStatusEnum;
    doctorStatusTypes:any[]=[];
    selectYear!:number;


    constructor(
      private formBuilder: FormBuilder,
      private route: ActivatedRoute,
      private router: Router,
      private doctorProfileService:DoctorProfilesService,
      private officeService: OfficesService,
      private specializationService: SpecializationService,
    )
    {
      let now = new Date();
      now.setFullYear(now.getFullYear() - 18);
      this.maxDate = now;
      let s = this.doctorStatusEnum[0]
      this.doctorStatusTypes = Object.keys(DoctorStatusEnum).filter(x=>isNaN(parseInt(x)));
      this.doctorStatusTypes.forEach(element => {
        let i = DoctorStatusEnum[element]
        let b = DoctorStatusEnum[element.key]
      });
    }

    ngOnInit(): void {
      this.createForm = new FormGroup({
        "firstName": new FormControl("", [Validators.required]),
        "lastName": new FormControl("", [Validators.required]),
        "middleName": new FormControl(null),
        "dateOfBirth": new FormControl("", [Validators.required]),
        "email": new FormControl("", [Validators.email,Validators.required]),
        "careerStartYear": new FormControl(null,[Validators.required,Validators.pattern('[1-2][0-9]{3}'),Validators.max( new Date().getFullYear()), Validators.min(1960)]),
        "status": new FormControl(null),
        "photo": new FormControl(null),
        "office": new FormControl("", [Validators.required]),
        "specialization": new FormControl("", [Validators.required]),
        "phoneNumber": new FormControl("", [Validators.required,Validators.pattern('^\\+[0-9][0-9]*')]),
      });

      this.officeService.getOffices().subscribe((data)=>{
        this.offices = data;
     })
     this.specializationService.getSpecializations().subscribe((data)=>{
        this.specializations = data;
     })
     
    }

    get f() { return this.createForm.controls; }

    onSubmit() {
      this.submitted = true;

      if (this.createForm.invalid) {
        return;
      }
      const time =(new Date(this.createForm.get('dateOfBirth')?.value)).toUTCString();

      const formData = new FormData();

      formData.append("firstName",this.createForm.get('firstName')?.value)
      formData.append("lastName",this.createForm.get('lastName')?.value)
      if(this.createForm.get('middleName')?.value != null)
        formData.append("middleName",this.createForm.get('middleName')?.value)
      formData.append("dateOfBirth",new Date(this.createForm.get('dateOfBirth')?.value).toUTCString())
      formData.append("careerStartYear",this.createForm.get('careerStartYear')?.value)
      formData.append("status",this.createForm.get('status')?.value)
      if(this.createForm.get('photo')?.value != null)
        formData.append("photo",this.createForm.get('photo')?.value)
      formData.append("officeId",this.createForm.get('office')?.value.id)
      formData.append("address",this.createForm.get('office')?.value.address)
      formData.append("specializationId",this.createForm.get('specialization')?.value.id)
      formData.append("specializationName",this.createForm.get('specialization')?.value.specializationName)
      formData.append("phoneNumber",this.createForm.get('phoneNumber')?.value)
      formData.append("email",this.createForm.get('email')?.value)

      this.doctorProfileService.create(formData).subscribe(
        data=>{
          console.log(data)
          let currentRole = JSON.parse(localStorage.getItem('currentUser')!).Role.toLowerCase();
          this.router.navigate([`${currentRole}/menu/doctors`]);
          
        },
        error=>
        {
          this.stop = true;
            this.error='Email already in use'
        }
      );
    }

    upload(files:any) {
      if (files.length === 0)
        return;

      for (const file of files) {
        this.createForm.get('photo')?.setValue(file);
      }
    }

    onSelect(files:any) {
      if (files.length === 0)
        return;

      const file = files[0];
      this.createForm.patchValue({photo:file});
      this.createForm.get('photo')?.updateValueAndValidity();
      const reader = new FileReader();
      reader.onload = () => {
        this.imagePreview = reader.result?.toString()!;
      };
      reader.readAsDataURL(file);

    }

    goBack(){
      let currentRole = JSON.parse(localStorage.getItem('currentUser')!).Role.toLowerCase();
      this.router.navigate([`${currentRole}/menu/doctors`]);
    }

}