import { CommonModule } from '@angular/common';
import { Component, inject, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Login } from '../login/login';
import { HomeStore } from '../../stores/HomeStore';
import { NewMovement } from '../movements/new-movement/new-movement';
import { Toast } from '../toast/toast';
import { AuthStore } from '../../stores/AuthStore';

@Component({
  selector: 'home-root',
  imports: [
    RouterOutlet,
    Toast,
    CommonModule,
    Login,
    NewMovement
  ],
  templateUrl: './home.html'
})
export class Home {
  protected readonly title = signal('SmartPocket');

  homeStore = inject(HomeStore);
  authStore = inject(AuthStore);

  isLoggedIn = this.authStore.select.isLoggedIn;

  logOut() {

  }
}
