import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css'],
})
export class NavComponent implements OnInit {
  model: any = {};

  constructor(public accountService: AccountService, private router: Router) {}

  ngOnInit(): void {}

  Login() {
    this.accountService.Login(this.model).subscribe({
      next: (res) => {
        this.router.navigateByUrl('/members');
        console.log(res);
      },
      error: (err) => {
        console.log(err);
      },
    });
  }

  Logout() {
    this.accountService.Logout();
    this.router.navigateByUrl('/');
  }
}
