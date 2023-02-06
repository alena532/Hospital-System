import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl,ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { AuthService } from 'src/app/_services/auth.service';
import {Router} from "@angular/router";


@Component({
  selector: 'app-menu',
  templateUrl: 'menu.component.html',
  styleUrls: ['./menu.component.css']
})
export class MenuComponent implements OnInit {

  constructor(
   private authService:AuthService,
   private router: Router,
  ) { }

  ngOnInit() {
  }

  logout(){
    this.authService.logout()
    this.router.navigate(['/login']);

  }
}