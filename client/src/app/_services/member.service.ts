import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Member } from '../_models/IMember';

@Injectable({
  providedIn: 'root',
})
export class MemberService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  GetMembers() {
    return this.http.get<Member[]>(this.baseUrl + 'list');
  }

  GetMember(username: string) {
    return this.http.get<Member>(this.baseUrl + username);
  }

  UpdateMember(member: Member) {
    return this.http.put(this.baseUrl + 'update', member);
  }
}
