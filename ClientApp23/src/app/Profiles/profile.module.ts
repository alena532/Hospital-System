import { NgModule } from '@angular/core';
import { ReactiveFormsModule,FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { CreateProfileComponent } from './Components/Create/create.profile.component';
import { DatePickerModule } from '@syncfusion/ej2-angular-calendars';
import { ProfilesService } from './_services/profiles.service';
import { ProfilesRoutingModule } from './profile-routing.module';
import { CommonModule } from '@angular/common';
import { ButtonModule } from '@syncfusion/ej2-angular-buttons';


@NgModule({
  declarations: [
    CreateProfileComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    DatePickerModule,
    ProfilesRoutingModule,
    ButtonModule,
    FormsModule,
  ],
  providers: [
    ProfilesService
  ]
})
export class ProfilesModule { }
