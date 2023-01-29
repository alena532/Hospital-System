import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MenuComponent } from './Components/Menu/menu.component';
import { GetDoctorsComponent } from '../ProfilesService/Components/GetDoctors/get.doctors.component';
import { UpdateDoctorComponent } from '../ProfilesService/Components/UpdateDoctor/update.doctor.component';
const routes: Routes = [
  {path: '',component:MenuComponent},
  {path: 'menu',component:MenuComponent },
  {path: 'doctors',component:GetDoctorsComponent },
  {path: 'doctor/:id',component:UpdateDoctorComponent },
  
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ReceptionistsRoutingModule { }
