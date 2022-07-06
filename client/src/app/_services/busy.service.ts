import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class BusyService {
  busyRequestCount: number = 0;
  constructor() {}

  Busy() {
    this.busyRequestCount++;
  }

  Idle() {
    this.busyRequestCount--;
    if (this.busyRequestCount <= 0) {
      this.busyRequestCount = 0;
    }
  }
}
