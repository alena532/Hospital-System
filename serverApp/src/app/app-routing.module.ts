import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './Components/Login/login.component';

const routes: Routes = [
  { path: '', component: LoginComponent },
  { path: 'login', component: LoginComponent },
  {
    path: 'receptionist', loadChildren: () => import('./Receptionist/receptionist.module').then(m=>m.ReceptionistsModule)
  },
  {
    path: 'doctor', loadChildren: () => import('./Doctor/doctor.module').then(m=>m.DoctorsModule)
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
