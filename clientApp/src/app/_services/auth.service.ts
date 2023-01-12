import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { HttpClientModule } from '@angular/common/http';
import {map, Observable, of, throwError} from "rxjs";
import { catchError, finalize, tap } from 'rxjs/operators';
import {User} from "../_models/User"
import { FormGroupDirective } from '@angular/forms';
import { switchMap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private accessPointUrl: string = 'https://localhost:5002/api/Auth';
  userId!: string;
  currentUser: User | undefined;
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
        return this.http.get<User>(`https://localhost:7097/api/Accounts/CheckPatientAccountBeforeProfileLogin/${userInfo['id']}`)
          .pipe(
            map((data:any)=>{
              console.log("in login")
              console.log(this.isAuthenticated)
              let patient = data['patient'];
              this.currentUser = new User(data['email'],patient['id'],patient['firstName'],patient['lastName'],patient['middleName'],data['phoneNumber'],patient['dateOfBirth'],patient['accountId']);
              localStorage.setItem("currentUser",JSON.stringify(this.currentUser));
              this.isAuthenticated = true;
              localStorage.setItem("isAuthenticated",JSON.stringify(this.isAuthenticated));
            },
          )
        )
      })
    )
  }

  getIsAuthenticated():Boolean{
    return JSON.parse(localStorage.getItem('isAuthenticated')!);
  }
  getCurrentUser():User{
    return JSON.parse(localStorage.getItem('currentUser')!);
  }

  register(email: string,password: string){
    let roleId = '3968115f-b6a0-40c1-1e44-08dadeca5a9a';
    return this.http.post<any>('https://localhost:7097/api/PatientProfiles/CreateAccount', { email, password, roleId})
    .pipe(
      tap((AccountId)=> {
        this.http.post<any>('https://localhost:7097/api/Mail/SendToPatient',AccountId ).subscribe()
      })
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
    this.currentUser = undefined;
    this.isAuthenticated = false;
    localStorage.removeItem('currentUser');
    localStorage.removeItem('isAuthenticated');
    localStorage.removeItem('token');
    localStorage.removeItem('refresh_token');
  }

}
