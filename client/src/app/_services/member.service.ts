import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Member } from '../_models/IMember';

const httpOptions = {
  headers: new HttpHeaders({
    Authorization: 'Bearer ' + JSON.parse(localStorage.getItem('user'))?.token,
  }),
};

@Injectable({
  providedIn: 'root',
})
export class MemberService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  GetMembers() {
    return this.http.get<Member[]>(this.baseUrl + 'list', httpOptions);
  }

  GetMember(username: string) {
    return this.http.get<Member>(this.baseUrl + username, httpOptions);
  }
}
