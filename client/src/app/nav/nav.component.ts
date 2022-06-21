import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css'],
})
export class NavComponent implements OnInit {
  model: any = {};
  loggedIn: boolean;

  constructor(private accountService: AccountService) {}

  ngOnInit(): void {
    this.getCurrentUser();
  }

  Login() {
    this.accountService.Login(this.model).subscribe({
      next: (res) => {
        console.log(res), (this.loggedIn = true);
      },
      error: (err) => console.log(`Error  + ${err}`),
    });
  }

  Logout() {
    this.accountService.Logout();
    this.loggedIn = false;
  }

  getCurrentUser() {
    this.accountService.currentUser$.subscribe({
      next: (user) => {
        this.loggedIn = !!user;
      },
      error: (err) => {
        console.log(err);
      },
    });
  }
}
