import { NgModule } from '@angular/core';
import { ReactiveFormsModule,FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { CreateProfileComponent } from './Components/CreateProfile/create.profile.component';
import { PatientProfilesService } from './_services/patientProfile.service';
import { ProfilesRoutingModule } from './profile-routing.module';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatNativeDateModule } from '@angular/material/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';    
import { AccountsService } from './_services/account.service';
import { HttpClientModule } from '@angular/common/http';
import { NgbPaginationModule } from '@ng-bootstrap/ng-bootstrap';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { GetDoctorsComponent } from './Components/GetDoctors/get.doctors.component';
import { DoctorProfilesService } from './_services/doctorProfile.service';
import { NgxPaginationModule } from 'ngx-pagination';
@NgModule({
  declarations: [
    CreateProfileComponent,
    GetDoctorsComponent
  ],
  imports: [
    NgbModule,
    CommonModule,
    HttpClientModule,
    ReactiveFormsModule,
    ProfilesRoutingModule,
    FormsModule,
    MatInputModule,
    MatFormFieldModule,
    MatNativeDateModule,
    MatButtonToggleModule,
    MatDatepickerModule,
    NgxPaginationModule
  ],
  providers: [
    PatientProfilesService,
    AccountsService,
    DoctorProfilesService
  ]
})
export class ProfilesModule { }
