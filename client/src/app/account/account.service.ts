import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, of, ReplaySubject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IUser } from '../shared/models/user';
import { map } from 'rxjs/operators';
import { Router } from '@angular/router';
import { IAddress } from '../shared/models/address';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  baseUrl = environment.apiUrl;
  // BehaviorSubject make a problem because it should initiate the subject
  // so when we reload the page and we are logged in so the user subject will be null and will redirect to login again!

  // 1 number of the values that we need subject to hold
  private currentUserSource = new ReplaySubject<IUser>(1); // hold 1 user

  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient, private router: Router) {}

  loadCurrentUser(token: string): Observable<any> {
    if (token === null) {
      this.currentUserSource.next(null!);
      return of(null);
    }

    let header = new HttpHeaders();
    header = header.set('Authorization', `Bearer ${token}`);

    return this.http
      .get<IUser>(this.baseUrl + 'account', { headers: header })
      .pipe(
        map((user: IUser) => {
          if (user) {
            localStorage.setItem('token', user.token);
            this.currentUserSource.next(user); // update the observable and pass the user
          }
        })
      );
  }

  login(values: any) {
    return this.http.post<IUser>(this.baseUrl + 'account/login', values).pipe(
      map((user: IUser) => {
        if (user) {
          localStorage.setItem('token', user.token);
          this.currentUserSource.next(user);
        }
      })
    );
  }

  register(values: any) {
    return this.http
      .post<IUser>(this.baseUrl + 'account/register', values)
      .pipe(
        map((user: IUser) => {
          if (user) {
            localStorage.setItem('token', user.token);
            this.currentUserSource.next(user);
          }
        })
      );
  }

  logout() {
    localStorage.removeItem('token');
    this.currentUserSource.next(null!);
    this.router.navigateByUrl('/');
  }

  checkEmailExist(email: string) {
    return this.http.get(this.baseUrl + 'account/emailExists?email=' + email);
  }

  getUserAddress() {
    return this.http.get<IAddress>(this.baseUrl + 'account/address');
  }

  updateUserAddress(address: IAddress) {
    return this.http.put<IAddress>(this.baseUrl + 'account/address', address);
  }
}
