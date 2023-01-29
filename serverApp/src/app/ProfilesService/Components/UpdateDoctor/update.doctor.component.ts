import {Component, OnInit,ViewChild} from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl,ReactiveFormsModule } from '@angular/forms';
import {ActivatedRoute,Router} from "@angular/router";
import { Doctor } from '../../_models/Doctor';
import { Pagination } from '../../_models/Pagination';
import {DomSanitizer} from '@angular/platform-browser';
import { DoctorProfilesService } from '../../_services/doctorProfile.service';
import { Office } from '../../../OfficesService/_models/Office';
import { OfficesService } from '../../../OfficesService/_services/office.service';
import { Specialization } from 'src/app/ServicesService/_models/Specialization';
import { GetDoctorProfile } from '../../_models/responses/GetDoctorProfile';
import { SpecializationService } from 'src/app/ServicesService/_services/specialization.service';
import { DoctorStatusEnum } from '../../_models/DoctorStatusEnum';
import { MatDatepicker } from '@angular/material/datepicker';

@Component({
    selector: 'doctor-update',
    providers: [],
    templateUrl: './update.doctor.component.html',
    styleUrls: ['./update.doctor.component.css']
  })
  export class UpdateDoctorComponent implements OnInit {
    editForm!: FormGroup;
    public placeholder: String = 'Date of Birth';
    error = '';
    stop = false;
    imagePreview!: string;
    submitted = false;
    loading = false;
    maxDate:Date;
    doctor!:GetDoctorProfile;
    offices!:Office[];
    specializations!:Specialization[];
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
      this.editForm = new FormGroup({
        "firstName": new FormControl("", [Validators.required]),
        "lastName": new FormControl("", [Validators.required]),
        "middleName": new FormControl(null),
        "dateOfBirth": new FormControl("", [Validators.required]),
        "careerStartYear": new FormControl(null,[Validators.required,Validators.pattern('[1-2][0-9]{3}'),Validators.max( new Date().getFullYear()), Validators.min(1960)]),
        "status": new FormControl(null),
        "photo": new FormControl(null),
        "id": new FormControl(this.route.snapshot.paramMap.get("id")),
        "office": new FormControl(),
        "specialization": new FormControl(),
      });



      this.doctorProfileService.getById(this.editForm.get('id')?.value).subscribe(
        data=>{
            this.editForm.patchValue(data)
            this.doctor = data;
            let office =new Office(data['officeId'],data['address'])
            this.editForm.get('office')?.setValue(office)
            let specialization=new Specialization(data['specializationId'],data['specializationName'])
            this.editForm.get('specialization')?.setValue(specialization)
        },
        error=>{
            this.stop = true;
            this.error ="Doctor not found"
        }
      )
      this.officeService.getOffices().subscribe((data)=>{
        this.offices = data;
     })
     this.specializationService.getSpecializations().subscribe((data)=>{
        this.specializations = data;
     })
      if (localStorage.getItem('updateDoctor')!=null)
        this.editForm.patchValue(JSON.parse(localStorage.getItem('updateDoctor')!))
    }

    get f() { return this.editForm.controls; }

    onSubmit() {
      this.submitted = true;

      if (this.editForm.invalid) {
        return;
      }
      const time =(new Date(this.editForm.get('dateOfBirth')?.value)).toUTCString();

      const formData = new FormData();

      formData.append("id",this.editForm.get('id')?.value)
      formData.append("firstName",this.editForm.get('firstName')?.value)
      formData.append("lastName",this.editForm.get('lastName')?.value)
      if(this.editForm.get('middleName')?.value != null)
        formData.append("middleName",this.editForm.get('middleName')?.value)
      formData.append("dateOfBirth",new Date(this.editForm.get('dateOfBirth')?.value).toUTCString())
      formData.append("careerStartYear",this.editForm.get('careerStartYear')?.value)
      formData.append("status",this.editForm.get('status')?.value)
      if(this.editForm.get('photo')?.value != null)
        formData.append("photo",this.editForm.get('photo')?.value)
      formData.append("officeId",this.editForm.get('office')?.value.id)
      formData.append("address",this.editForm.get('office')?.value.address)
      formData.append("specializationId",this.editForm.get('specialization')?.value.id)
      formData.append("specializationName",this.editForm.get('specialization')?.value.specializationName)

      this.doctorProfileService.update(formData).subscribe();
    }

    upload(files:any) {
      if (files.length === 0)
        return;

      for (const file of files) {
        this.editForm.get('photo')?.setValue(file);
      }
    }

    onSelect(files:any) {
      if (files.length === 0)
        return;

      const file = files[0];
      this.editForm.patchValue({photo:file});
      this.editForm.get('photo')?.updateValueAndValidity();
      const reader = new FileReader();
      reader.onload = () => {
        this.imagePreview = reader.result?.toString()!;
      };
      reader.readAsDataURL(file);

    }

}