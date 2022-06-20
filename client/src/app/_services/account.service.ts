import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  baseUrl = 'https://localhost:7104/api/';

  constructor(private http: HttpClient) {}

  Login(model: any) {
    return this.http.post(this.baseUrl + 'login', model);
  }
}
