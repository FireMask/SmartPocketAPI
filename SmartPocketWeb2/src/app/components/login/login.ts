import { Component, inject } from '@angular/core';
import { AuthService } from '../../services/auth';
import { User } from '../../models/user';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-login',
  imports: [
    FormsModule,
    CommonModule
  ],
  templateUrl: './login.html',
  styles: ``
})
export class Login {

  showRegister: boolean = false;

  user: Partial<User> = {
    email: '',
    password: ''
  };

  registerUser: Partial<User> = {
    email: '',
    password: '',
    name: ''
  };

  private authService = inject(AuthService);

  login() {
    this.authService.login(this.user).subscribe({
      next: (response) => {
        localStorage.setItem('auth_token', response.data.token);
        console.log('Login successful:', response);
      },
      error: (error) => {
        console.error('Login failed:', error);
      }
    });
  }

  register() {

  }
  
}
