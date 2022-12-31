import { NgModule } from '@angular/core';
import { ReactiveFormsModule,FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { CreateProfileComponent } from './Components/Create/create.profile.component';
import { ProfilesService } from './_services/profiles.service';
import { ProfilesRoutingModule } from './profile-routing.module';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from 
    '@angular/material/button';
import { MatButtonToggleModule } from 
    '@angular/material/button-toggle';
import { MatDatepickerModule } from 
    '@angular/material/datepicker';
import { MatInputModule } from 
    '@angular/material/input';
import { MatFormFieldModule } from 
    '@angular/material/form-field';
import { MatNativeDateModule } from 
    '@angular/material/core';
    import { BrowserAnimationsModule } from '@angular/platform-browser/animations';    
import { AccountsService } from './_services/account.service';
@NgModule({
  declarations: [
    CreateProfileComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    ProfilesRoutingModule,
    FormsModule,
    MatInputModule,
    MatFormFieldModule,
    MatNativeDateModule,
    MatButtonToggleModule,
    MatDatepickerModule
  ],
  providers: [
    ProfilesService,
    AccountsService
  ]
})
export class ProfilesModule { }
