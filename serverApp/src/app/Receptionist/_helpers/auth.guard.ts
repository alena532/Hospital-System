import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { GetDetailedReceptionistProfilesResponse } from 'src/app/_models/responses/GetDetailedReceptionistProfilesResponse';
import { AuthService } from 'src/app/_services/auth.service';

@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {
  constructor(
    private router: Router,
    private authService: AuthService
  ) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    if(this.authService.getCurrentUser().Role == "Receptionist")
        return true;

    this.router.navigate(['/']);
    return false;
  }
}