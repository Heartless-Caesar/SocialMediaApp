import { Component, OnInit } from '@angular/core';
import { Member } from 'src/app/_models/IMember';
import { MemberService } from 'src/app/_services/member.service';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css'],
})
export class MemberListComponent implements OnInit {
  members: Member[];

  constructor(private memberService: MemberService) {}

  ngOnInit(): void {
    this.loadMembers();
  }

  loadMembers() {
    this.memberService.GetMembers().subscribe((res) => {
      this.members = res;
      console.log(this.members);
    });
  }
}
