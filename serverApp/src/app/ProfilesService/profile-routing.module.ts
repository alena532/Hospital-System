import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { GetDoctorsComponent } from '../ProfilesService/Components/GetDoctors/get.doctors.component';
import { UpdateDoctorComponent } from './Components/UpdateDoctor/update.doctor.component';

const routes: Routes = [
  {path: 'doctors',component:GetDoctorsComponent },
  {path: 'doctor/:id',component:UpdateDoctorComponent },
  
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProfilesRoutingModule { }
