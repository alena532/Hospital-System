import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { HttpClientModule } from '@angular/common/http';
import {map, Observable, of, throwError} from "rxjs";
import { catchError, finalize, tap } from 'rxjs/operators';
import { FormGroupDirective } from '@angular/forms';
import { switchMap } from 'rxjs/operators';
import { GetDetailedReceptionistProfilesResponse } from '../_models/responses/GetDetailedReceptionistProfilesResponse';
import { GetDoctorProfilesResponse } from '../ProfilesService/_models/responses/GetDoctorProfilesResponse';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private accessPointUrl: string = 'https://localhost:5000/AuthApi/Auth';
  userId!: string;
  isAuthenticated: boolean = false;
  constructor(private http: HttpClient) {
  }

  login(email: string,password: string) {
    return this.http.post<any>(this.accessPointUrl+'/Login', { email, password })
    .pipe(
      switchMap((tokens) => {
        let userInfo = tokens['user'];
        this.storeTokens(tokens['tokens']);
        this.userId = userInfo['id'];
        switch(userInfo['role']){
          case 'Receptionist':
            return this.http.get<GetDetailedReceptionistProfilesResponse>(`https://localhost:5000/ProfilesApi/ReceptionistProfiles/UserId/${userInfo['id']}`)
            .pipe(
              map((data:GetDetailedReceptionistProfilesResponse)=>{
                let receptionist = data;
                receptionist.Role = 'Receptionist';
                localStorage.setItem("currentUser",JSON.stringify(receptionist));
              })
            )
            break;
          case 'Doctor':
            return this.http.get<boolean>(`https://localhost:5000/ProfilesApi/DoctorProfiles/CheckEmailConfirmation/${userInfo['id']}`)
            .pipe(
              map((data:boolean)=>{
                if (data == false)
                  throw new Error('Email isn`t confirmed')
                switchMap(()=>
                  this.http.get<GetDoctorProfilesResponse>(`https://localhost:5000/ProfilesApi/DoctorProfiles/UserId/${userInfo['id']}`)
                  .pipe(
                    map((data:GetDoctorProfilesResponse)=>{
                      let doctor = data ;
                      doctor.Role = 'Doctor';
                      localStorage.setItem("currentUser",JSON.stringify(doctor));
                    })
                  )
                )
              })
            )
            break;

        default:
          return this.http.get<GetDetailedReceptionistProfilesResponse>(`https://localhost:5000/ProfrilesApi/Accounts/CheckPatientAccountBeforeProfileLogin/${userInfo['id']}`)

        }
      })
    )
  }

  getIsAuthenticated():Boolean{
    return JSON.parse(localStorage.getItem('isAuthenticated')!);
  }
  getCurrentUser(){
    return JSON.parse(localStorage.getItem('currentUser')!);
  }

  register(email: string,password: string){
    let roleId = '3968115f-b6a0-40c1-1e44-08dadeca5a9a';
    return this.http.post<any>('https://localhost:5000/ProfrilesApi/PatientProfiles/CreateAccount', { email, password, roleId})
    .pipe(
      switchMap((AccountId : any) => {
        return this.http.post<any>('https://localhost:5000/ProfrilesApi/Mail/SendToPatient',AccountId )
      }
      )
    )
  }

  private storeTokens(tokens:any){
    console.log(tokens['token'])
    console.log(tokens['refreshToken'])
    localStorage.setItem('token',JSON.stringify(tokens['token']));
    localStorage.setItem('refresh_token',JSON.stringify(tokens['refreshToken']));
  }


  refresh(token: string,refreshToken: string){
    return this.http.post<any>(this.accessPointUrl+'/Refresh', { token, refreshToken }).pipe(
      tap((tokens)=> {
      this.storeTokens(tokens);
      }),
      catchError((error)=>{
        this.logout();
        return of(false);
      })
    )
  }


  logout(){
    this.isAuthenticated = false;
    localStorage.removeItem('currentUser');
    localStorage.removeItem('isAuthenticated');
    localStorage.removeItem('token');
    localStorage.removeItem('refresh_token');
  }

}
