import { NgModule } from '@angular/core';
import { ReactiveFormsModule,FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MatButtonToggleModule } from '@angular/material/button-toggle';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatNativeDateModule } from '@angular/material/core';
import { HttpClientModule } from '@angular/common/http';
import { NgxPaginationModule } from 'ngx-pagination';

import { DoctorProfilesService } from '../ProfilesService/_services/doctorProfile.service';
import { OfficesService } from '../OfficesService/_services/office.service';
import { ProfilesModule } from '../ProfilesService/profiles.module';
import { SpecializationService } from '../ServicesService/_services/specialization.service';
import {MatTabsModule} from '@angular/material/tabs';
import { MatButtonModule } from '@angular/material/button';
import { DoctorsRoutingModule } from './doctor-routing.module';
import { MenuComponent } from './Components/Menu/menu.component';
@NgModule({
  declarations: [
    MenuComponent
    
  ],
  imports: [
    ProfilesModule,
    CommonModule,
    HttpClientModule,
    ReactiveFormsModule,
    DoctorsRoutingModule,
    FormsModule,
    MatInputModule,
    MatFormFieldModule,
    MatNativeDateModule,
    MatButtonToggleModule,
    MatDatepickerModule,
    NgxPaginationModule,
    MatTabsModule,
    MatButtonModule
  ],
  providers: [
    DoctorProfilesService,
    OfficesService,
    SpecializationService,
  ]
})
export class DoctorsModule { }
