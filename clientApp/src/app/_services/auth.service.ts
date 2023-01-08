import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { HttpClientModule } from '@angular/common/http';
import {map, Observable, of, throwError} from "rxjs";
import { catchError, finalize, tap } from 'rxjs/operators';
import {User} from "../_models/User"
import { FormGroupDirective } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private accessPointUrl: string = 'https://localhost:5002/api/Auth';
  private loggedUser: User | any;
  private userId!:string | any;
  currentUser: User | undefined;
  constructor(private http: HttpClient) {

  }

  login(email: string,password: string) {
    return this.http.post<any>(this.accessPointUrl+'/Login', { email, password })
    .pipe(
      tap((tokens)=> {
      let userInfo = tokens['user'];
      this.userId = userInfo['id'];
      console.log(`in method login ${tokens}`);
      this.storeTokens(tokens['tokens']);
      this.http.get<User>(`https://localhost:7097/api/Accounts/CheckAccountBeforeProfileLogin/${this.userId}`)
      .subscribe(
        (data:any)=>{
          console.log(`in method checkUserBeforeLogin ${data}`);
          let patient = data['patient'];
          this.currentUser = new User(patient['firstName'],patient['lastName'],patient['id'],data['email']);
        }
      )
      }),
    )
    
    
  }
  checkUserBeforeLogin(){
    console.log(`in checkUserBeforeLogin ${this.userId}`)
    return this.http.get<User>(`https://localhost:7097/api/Accounts/CheckAccountBeforeProfileLogin/${this.userId}`)
    .pipe(
      map((data:any)=>{
        console.log(`in method checkUserBeforeLogin ${data}`);
        let patient = data['patient'];
        return new User(patient['firstName'],patient['lastName'],patient['id'],data['email']);
      }

      )
    )

  }

  register(email: string,password: string){
    let roleId = '3968115f-b6a0-40c1-1e44-08dadeca5a9a';
    return this.http.post<any>('https://localhost:7097/api/PatientProfiles/CreateAccount', { email, password, roleId})
    .pipe(
      tap((AccountId)=> {
        //let a={toEmail:AccountId.toEmail,accountId:AccountId.accountId}
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
    this.loggedUser = null;
    this.userId = null;
    localStorage.removeItem('token');
    localStorage.removeItem('refresh_token');
  }

}
