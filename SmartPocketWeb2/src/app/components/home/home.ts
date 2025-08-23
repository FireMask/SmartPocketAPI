import { CommonModule } from '@angular/common';
import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Login } from '../login/login';

@Component({
  selector: 'home-root',
  imports: [
    RouterOutlet,
    CommonModule,
    Login
  ],
  templateUrl: './home.html'
})
export class Home {
  protected readonly title = signal('SmartPocket');

  isLoading = false;

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
