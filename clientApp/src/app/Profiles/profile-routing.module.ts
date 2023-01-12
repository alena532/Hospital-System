import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CreateProfileComponent } from './Components/CreateProfile/create.profile.component';
import { GetDoctorsComponent } from './Components/GetDoctors/get.doctors.component';
import { GetProfileComponent } from './Components/ViewProfile/get.profile.component';

const routes: Routes = [
  {path: 'createProfile/:accountId',component:CreateProfileComponent},
  {path: 'doctors',component:GetDoctorsComponent },
  {path:'profile',component:GetProfileComponent}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProfilesRoutingModule { }
