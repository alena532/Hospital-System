import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MenuComponent } from './Components/Menu/menu.component';

const routes: Routes = [
  {path: '',redirectTo: 'menu'},
  {path: 'menu',component:MenuComponent,
  
},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DoctorsRoutingModule { }
