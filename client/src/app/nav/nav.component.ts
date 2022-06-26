import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css'],
})
export class NavComponent implements OnInit {
  model: any = {};

  constructor(
    public accountService: AccountService,
    private router: Router,
    private toastrService: ToastrService
  ) {}

  ngOnInit(): void {}

  Login() {
    this.accountService.Login(this.model).subscribe({
      next: (res) => {
        this.router.navigateByUrl('/list');
        console.log(res);
      },
      error: (err) => {
        console.log(err), this.toastrService.error(err.error);
      },
    });
  }

  Logout() {
    this.accountService.Logout();
    this.router.navigateByUrl('/');
  }
}
