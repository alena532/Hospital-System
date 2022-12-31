import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { HttpClientModule } from '@angular/common/http';
import {map, Observable, of, throwError} from "rxjs";
import { catchError, finalize, tap } from 'rxjs/operators';
import {User} from "../_models/User"
import { FormGroupDirective } from '@angular/forms';

@Injectable()
export class AuthService {

  private accessPointUrl: string = 'https://localhost:5002/api/Auth';
  private loggedUser: User | any;

  //currentUser: User | undefined;
  constructor(private http: HttpClient) {

  }

  login(email: string,password: string) {
    return this.http.post<any>(this.accessPointUrl+'/Login', { email, password })
    .pipe(
      tap((tokens)=> {
      this.storeTokens(tokens);
      let userInfo = tokens['user'];
      this.loggedUser = new User(userInfo['id'],userInfo['email']);
      }),
    )
  }

  register(email: string,password: string){
    let roleId = '3968115f-b6a0-40c1-1e44-08dadeca5a9a';
    return this.http.post<any>('https://localhost:7097/api/PatientProfiles/CreateAccount', { email, password, roleId})
    .pipe(
      tap((accountId)=> {
        this.http.post<any>('https://localhost:7097/api/Mail/SendToPatient', { accountId })
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
    this.loggedUser = null;
    localStorage.removeItem('token');
    localStorage.removeItem('refresh_token');
  }

}
