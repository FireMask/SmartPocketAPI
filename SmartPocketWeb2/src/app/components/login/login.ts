import { Component, inject } from '@angular/core';
import { AuthService } from '../../services/auth';
import { User } from '../../models/auth/user';
import { CommonModule } from '@angular/common';
import { AbstractControl, FormControl, FormGroup, FormsModule, ReactiveFormsModule, ValidatorFn, Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule
  ],
  templateUrl: './login.html',
  styles: ``
})
export class Login {

  showRegister: boolean = false;

  loginForm = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required])
  });

  get f(){
    return this.loginForm.controls;
  }

  registerForm = new FormGroup({
    name: new FormControl('', [Validators.required]),
    email: new FormControl('', [Validators.required, Validators.email]),
    password: new FormControl('', [
      Validators.required,
      Validators.minLength(6),
      Validators.pattern('^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)[a-zA-Z\\d]{6,20}$')
    ]) // At least one uppercase letter, one lowercase letter, and one digit
  });

  get f2(){
    return this.registerForm.controls;
  }

  private authService = inject(AuthService);

  login() {
    const user: Partial<User> = {
      email: this.loginForm.value.email || '',
      password: this.loginForm.value.password || ''
    };
    this.authService.login(user).subscribe({
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
    const user: Partial<User> = {
      email: this.registerForm.value.email || '',
      password: this.registerForm.value.password || '',
      name: this.registerForm.value.name || ''
    };
    this.authService.register(user).subscribe({
      next: (response) => {
        console.log('Registration successful:', response);
      },
      error: (error) => {
        console.error('Registration failed:', error);
      }
    });
  }
  
}
