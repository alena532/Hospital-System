import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MenuComponent } from './Components/Menu/menu.component';
import { GetDoctorsComponent } from '../ProfilesService/Components/GetDoctors/get.doctors.component';
import { UpdateDoctorComponent } from '../ProfilesService/Components/UpdateDoctor/update.doctor.component';
import { AuthGuard } from './_helpers/auth.guard';
import { CreateDoctorComponent } from '../ProfilesService/Components/CreateDoctor/create.doctor.component';
const routes: Routes = [
  {path: '',redirectTo: 'menu'},
  {path: 'menu',component:MenuComponent,canActivate: [AuthGuard],
  children:[
    {path: 'doctors',component:GetDoctorsComponent },
    {path: 'doctor/create',component:CreateDoctorComponent },
    {path: 'doctor/:id',component:UpdateDoctorComponent },
  ]
},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ReceptionistsRoutingModule { }
