import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter();
  model: any = {};

  constructor(private accountService: AccountService) {}

  ngOnInit(): void {}

  register() {
    this.accountService.Register(this.model).subscribe({
      next: (res) => {
        console.log(res);
        this.cancel();
      },
      error: (err) => {
        console.log(err);
      },
    });
    console.log(this.model);
  }

  cancel() {
    this.cancelRegister.emit(false);
  }
}
