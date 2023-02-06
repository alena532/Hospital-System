import {Injectable, Type} from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { AuthService} from '../_services/auth.service'
import {Router} from "@angular/router";
import { catchError, filter, switchMap, take } from 'rxjs/operators';


@Injectable()
export class JwtInterceptor implements HttpInterceptor {
  constructor(private authService:AuthService,private router: Router) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    console.log(JSON.parse(localStorage.getItem('token')!));
    console.log(localStorage.getItem('refresh_token'));
    let expired = true;
    if(localStorage.getItem('token')){
      const expiry = (JSON.parse(atob(localStorage.getItem('token')!.split('.')[1]))).exp;

      if(Date.now() < expiry*1000){
        expired = false;
      }
    }
    if(localStorage.getItem('token') && !expired) {
      let token = JSON.parse(localStorage.getItem('token')! || '{}');
      console.log(token)
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${token}`
      }
    });
    }


    return next.handle(request).pipe(
      catchError((error) => {
        console.log(1)
        if(error instanceof HttpErrorResponse && error.status === 401){
          return this.handle401Error(request,next);
        } else{
          return throwError (error);
        }
      })
      
    )
  }

  private handle401Error(request: HttpRequest<any>,next:HttpHandler){
    return this.authService.refresh(JSON.parse(localStorage.getItem('token')!),JSON.parse(localStorage.getItem('refresh_token')!)).pipe(
      switchMap(()=>{
        return next.handle(request.clone({
          setHeaders: {
            Authorization: `Bearer ${localStorage.getItem('token')!}`
          }
        }))
      }),
      catchError((error)=>{
        this.router.navigate(['login'])
        return throwError(error);
      })
    );
    
  }
  
}
