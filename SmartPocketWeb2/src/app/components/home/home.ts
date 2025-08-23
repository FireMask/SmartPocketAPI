import { CommonModule } from '@angular/common';
import { Component, inject, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Login } from '../login/login';
import { HomeStore } from '../../stores/HomeStore';
import { NewMovementStore } from '../../stores/NewMovementStore';
import { NewMovement } from '../movements/new-movement/new-movement';

@Component({
  selector: 'home-root',
  imports: [
    RouterOutlet,
    CommonModule,
    Login,
    NewMovement
  ],
  templateUrl: './home.html'
})
export class Home {
  protected readonly title = signal('SmartPocket');

  homeStore = inject(HomeStore);
  newMovementStore = inject(NewMovementStore);

  isLoggedIn(): boolean {
    const token = localStorage.getItem('auth_token');
    if (token) {
      return true;
    }
    return false;
  }

  logOut() {
    localStorage.removeItem('auth_token');
  }
}
