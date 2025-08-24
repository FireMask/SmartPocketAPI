import { Injectable, signal, computed, inject } from '@angular/core';
import { User } from '../models/auth/user';
import { AuthService } from '../services/auth';
import { NotificationsStore } from './NotificationStore';
import { parseApiError } from '../helpers/error';
import { AuthTokenStore } from './AuthTokenStore';

@Injectable({ providedIn: 'root' })
export class AuthStore {

    private authService = inject(AuthService);
    private notify = inject(NotificationsStore);
    private tokenRef = inject(AuthTokenStore);

    private _user = signal<Partial<User> | null>(null);
    private _isLoggedIn = signal<boolean>(false);

    readonly select = {
        user: this._user.asReadonly(),
        isLoggedIn: this._isLoggedIn.asReadonly(),
        token: this.tokenRef.token.asReadonly()
    };

    constructor() {
        const token = localStorage.getItem('auth_token');
        if (token) {
            this.setToken(token);
            this._isLoggedIn.set(true);
        }

        const user = localStorage.getItem('auth_user');
        if (user) {
            this.setUser(JSON.parse(user));
        }
    }

    setUser(u: Partial<User> | null) {
        this._user.set(u);
    }

    setToken(t: string | null) {
        this.tokenRef.token.set(t);
    }

    login(user: Partial<User>) {
        this.authService.login(user).subscribe({
            next: (response) => {
                localStorage.setItem('auth_token', response.data.token);
                localStorage.setItem('auth_user', JSON.stringify(response.data.user));
                this.setToken(response.data.token);
                this.setUser(response.data.user);
                this._isLoggedIn.set(true);
            },
            error: (error) => {
                this.notify.error(parseApiError(error));
            }
        });
    }

    logOut() {
        localStorage.removeItem('auth_token');
        this.setToken(null);
        this.setUser(null);
        this._isLoggedIn.set(false);
    }

    register(user: Partial<User>) {
        this.authService.register(user).subscribe({
            next: (response) => {
                this.notify.success('Registration successful! You can now log in.');
            },
            error: (error) => {
                this.notify.error(parseApiError(error));
            }
        });
    }
}
