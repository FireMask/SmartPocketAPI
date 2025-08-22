import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AxiosService } from '../helpers/axios';
import { AuthToken, User } from '../models/user';
import { ApiResponse } from '../models/auth';

@Injectable({
    providedIn: 'root'
})
export class AuthService {
    constructor(private axiosInstance: AxiosService) {}

    getUser(): Observable<ApiResponse<User>> {
        return this.axiosInstance.get<User>("/user");
    }

    login(user: Partial<User>): Observable<ApiResponse<AuthToken>> {
        return this.axiosInstance.post<AuthToken>('/auth/login', user);
    }

    register(newUser: User): Observable<ApiResponse<User>> {
        return this.axiosInstance.post<User>('/auth/create', newUser);
    }
}