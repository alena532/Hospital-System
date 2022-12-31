import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CreateProfileComponent } from './Components/Create/create.profile.component';


const routes: Routes = [
  {path: 'createProfile',component:CreateProfileComponent},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProfilesRoutingModule { }
