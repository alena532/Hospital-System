import { NgModule } from '@angular/core';
import { ReactiveFormsModule,FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatNativeDateModule } from '@angular/material/core';
import { HttpClientModule } from '@angular/common/http';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NgxPaginationModule } from 'ngx-pagination';
import { GetDoctorsComponent } from '../ProfilesService/Components/GetDoctors/get.doctors.component';
import { DoctorProfilesService } from '../ProfilesService/_services/doctorProfile.service';
import { OfficesService } from '../OfficesService/_services/office.service';
import { UpdateDoctorComponent } from './Components/UpdateDoctor/update.doctor.component';

@NgModule({
  declarations: [
    GetDoctorsComponent,
    UpdateDoctorComponent
    
  ],
  imports: [
    NgbModule,
    CommonModule,
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule,
    MatInputModule,
    MatFormFieldModule,
    MatNativeDateModule,
    MatButtonToggleModule,
    MatDatepickerModule,
    NgxPaginationModule
  ],
  providers: [
    DoctorProfilesService,
    OfficesService
    
  ],
  exports:[
    GetDoctorsComponent
  ]
})
export class ProfilesModule { }
