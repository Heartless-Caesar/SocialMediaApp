import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Member } from 'src/app/_models/IMember';
import { MemberService } from 'src/app/_services/member.service';

@Component({
  selector: 'app-member-detail',
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.css'],
})
export class MemberDetailComponent implements OnInit {
  member: Member;

  constructor(
    private memberService: MemberService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.loadMember();
  }

  loadMember() {
    this.memberService
      .GetMember(this.route.snapshot.paramMap.get('username'))
      .subscribe((member) => {
        this.member = member;
      });
  }
}
