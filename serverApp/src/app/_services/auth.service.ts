import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { HttpClientModule } from '@angular/common/http';
import {map, Observable, of, throwError} from "rxjs";
import { catchError, finalize, tap } from 'rxjs/operators';
import { FormGroupDirective } from '@angular/forms';
import { switchMap } from 'rxjs/operators';
import { Receptionist } from '../_models/Receptionist';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private accessPointUrl: string = 'https://localhost:5000/api/Auth';
  userId!: string;
  isAuthenticated: boolean = false;
  constructor(private http: HttpClient) {
  }

  login(email: string,password: string) {
    return this.http.post<any>(this.accessPointUrl+'/Login', { email, password })
    .pipe(
      switchMap((tokens) => {
        let userInfo = tokens['user'];
        console.log(`in method login ${tokens}`);
        this.storeTokens(tokens['tokens']);
        this.userId = userInfo['id'];
        switch(userInfo['role']){
          case 'Receptionist':
            return this.http.get<Receptionist>(`https://localhost:7097/api/ReceptionistProfiles/UserId/${userInfo['id']}`)
            .pipe(
              map((data:Receptionist)=>{
                let receptionist = data as Receptionist;
                receptionist.Role = 'Receptionist';
                localStorage.setItem("currentUser",JSON.stringify(receptionist));
              }
              )
            )
            break;
        default:
          return this.http.get<Receptionist>(`https://localhost:7097/api/Accounts/CheckPatientAccountBeforeProfileLogin/${userInfo['id']}`)

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
    return this.http.post<any>('https://localhost:7097/api/PatientProfiles/CreateAccount', { email, password, roleId})
    .pipe(
      switchMap((AccountId : any) => {
        return this.http.post<any>('https://localhost:7097/api/Mail/SendToPatient',AccountId )
      }
      )
    )
  }

  private storeTokens(tokens:any){
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
