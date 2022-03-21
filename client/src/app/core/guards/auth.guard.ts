import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivate,
  Router,
  RouterStateSnapshot,
} from '@angular/router';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { AccountService } from 'src/app/account/account.service';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  constructor(private accountService: AccountService, private router: Router) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean> {
    // when we are in a context of router there is no need to subscribe and unsubscribe
    // to the observable , because the router it self will do that
    return this.accountService.currentUser$.pipe(
      map((auth) => {
        if (auth) {
          return true;
        }
        this.router.navigate(['account/login'], {queryParams: { returnUrl: state.url }});
        return false;
      })
    );
  }
}
