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
import { AppRoutingModule } from '../app-routing.module';
import { ProfilesRoutingModule } from './profile-routing.module';
import { MatIconModule } from '@angular/material/icon'
import { MatButtonModule } from '@angular/material/button';
import { CreateDoctorComponent } from './Components/CreateDoctor/create.doctor.component';
import {HTTP_INTERCEPTORS, HttpClient,HttpHandler, HttpHeaders} from "@angular/common/http";
import {JwtInterceptor} from "../_helpers/jwt.interceptor";
import { JwtHelperService, JwtModule, JWT_OPTIONS } from '@auth0/angular-jwt';
import { AuthService } from '../_services/auth.service';
@NgModule({
  declarations: [
    GetDoctorsComponent,
    UpdateDoctorComponent,
    CreateDoctorComponent
    
  ],
  imports: [
    NgbModule,
    CommonModule,
    ProfilesRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule,
    MatInputModule,
    MatFormFieldModule,
    MatNativeDateModule,
    MatButtonToggleModule,
    MatDatepickerModule,
    NgxPaginationModule,
    MatButtonModule,
    MatIconModule,
  ],
  providers: [
    DoctorProfilesService,
    OfficesService,
    AuthService,
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: JWT_OPTIONS, useValue: JWT_OPTIONS },
    
  ],
  exports:[
    GetDoctorsComponent
  ]
})
export class ProfilesModule { }
