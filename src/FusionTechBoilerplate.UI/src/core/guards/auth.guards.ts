import { CanActivate, Router } from '@angular/router';
import { AuthenticationService } from '../../app/authentication/authentication.service';
import { Injectable } from '@angular/core';

@Injectable()
export class AuthGuard implements CanActivate {

  constructor(
    private postUser: AuthenticationService,
    private router: Router) {}

  canActivate(): boolean {
    if (this.postUser.loggedIn()) {
      return true
    } else {
      this.router.navigate(['/auth/login']);
      return false
    }
  }
}