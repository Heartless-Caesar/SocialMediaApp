import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  private currentUserSource = new ReplaySubject<User>(1);
  currentUser$ = this.currentUserSource.asObservable();
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  Register(model: any) {
    return this.http.post(this.baseUrl + 'regiser', model).pipe(
      map((res: User) => {
        if (res) {
          const user = res;
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUserSource.next(user);
        }
      })
    );
  }

  Login(model: any) {
    return this.http.post(this.baseUrl + 'login', model).pipe(
      map((res: User) => {
        const user = res;
        if (user) {
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUserSource.next(user);
        }
      })
    );
  }

  setCurrentuser(user: User) {
    this.currentUserSource.next(user);
  }

  Logout() {
    localStorage.removeItem('user');
    this.currentUserSource.next(null);
  }
}
