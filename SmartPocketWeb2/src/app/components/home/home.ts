import { Component, inject, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Login } from '../login/login';
import { HomeStore } from '../../stores/HomeStore';
import { NewMovement } from '../movements/new-movement/new-movement';
import { Toast } from '../toast/toast';
import { AuthStore } from '../../stores/AuthStore';
import { UserMenu } from './user-menu/user-menu';

@Component({
  selector: 'home-root',
  imports: [
    RouterOutlet,
    UserMenu,
    Toast,
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
}
