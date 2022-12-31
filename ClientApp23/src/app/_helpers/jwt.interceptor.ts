import {Injectable, Type} from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor, HttpErrorResponse } from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';
import { AuthService } from '../_services/auth.service';
import jwt_decode from 'jwt-decode';
import {Router} from "@angular/router";
import {JwtHelperService} from '@auth0/angular-jwt';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
  constructor(private authService: AuthService, private jwtHelper: JwtHelperService, private router: Router) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    localStorage.removeItem('token')
    console.log(localStorage.getItem('token'));

    console.log(!localStorage.getItem('token'));
    console.log(localStorage.getItem('refresh_token'));
    if(!localStorage.getItem('token') || this.jwtHelper.isTokenExpired(localStorage.getItem('refresh_token'))) return next.handle(request);
    //if(this.jwtHelper.isTokenExpired(localStorage.getItem('refresh_token'))) return next.handle(request);
    let token = JSON.parse(localStorage.getItem('token')! || '{}');

    request = request.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`
      }
    });

    return next.handle(request).pipe(
      catchError((error) => {
        if(error instanceof HttpErrorResponse && error.status === 401){
          return this.handle401Error(request,next);
        } else{
          return throwError (error);
        }
      })
    )
  }

  private handle401Error(request: HttpRequest<any>,next:HttpHandler){

    this.authService.refresh(localStorage.getItem('token')!,localStorage.getItem('refresh_token')!)
    .pipe(
      catchError((error)=>
      this.router.navigate(['login'])
      ))
    return next.handle(request = request.clone({
      setHeaders: {
        Authorization: `Bearer ${localStorage.getItem('token')!}`
      }
    }));
  }
}
