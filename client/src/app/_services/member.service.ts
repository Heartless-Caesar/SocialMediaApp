import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { of } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Member } from '../_models/IMember';

@Injectable({
  providedIn: 'root',
})
export class MemberService {
  baseUrl = environment.apiUrl;
  members: Member[] = [];

  constructor(private http: HttpClient) {}

  GetMembers() {
    if (this.members.length > 0) return of(this.members);

    return this.http.get<Member[]>(this.baseUrl + 'list').pipe(
      map((members) => {
        this.members = members;
        return members;
      })
    );
  }

  GetMember(username: string) {
    const member = this.members.find((x) => x.username === username);

    if (member !== undefined) return of(member);

    return this.http.get<Member>(this.baseUrl + username);
  }

  UpdateMember(member: Member) {
    return this.http.put(this.baseUrl + 'update', member).pipe(
      map(() => {
        const index = this.members.indexOf(member);
        this.members[index] = member;
      })
    );
  }
}
