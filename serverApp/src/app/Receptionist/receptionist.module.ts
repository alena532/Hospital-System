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
import { ReceptionistsRoutingModule } from './receptionist-routing.module';
import { MenuComponent } from './Components/Menu/menu.component';
import { GetDoctorsComponent } from '../ProfilesService/Components/GetDoctors/get.doctors.component';
import { DoctorProfilesService } from '../ProfilesService/_services/doctorProfile.service';
import { OfficesService } from '../OfficesService/_services/office.service';
import { ProfilesModule } from '../ProfilesService/profiles.module';
import { SpecializationService } from '../ServicesService/_services/specialization.service';
@NgModule({
  declarations: [
    MenuComponent
    
  ],
  imports: [
    ProfilesModule,
    CommonModule,
    HttpClientModule,
    ReactiveFormsModule,
    ReceptionistsRoutingModule,
    FormsModule,
    MatInputModule,
    MatFormFieldModule,
    MatNativeDateModule,
    MatButtonToggleModule,
    MatDatepickerModule,
    NgxPaginationModule,
  ],
  providers: [
    DoctorProfilesService,
    OfficesService,
    SpecializationService
    
  ]
})
export class ReceptionistsModule { }
