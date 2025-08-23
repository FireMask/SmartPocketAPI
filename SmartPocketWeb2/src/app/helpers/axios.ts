import { Injectable } from '@angular/core';
import { from, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import axios, { AxiosRequestConfig, AxiosResponse } from 'axios';
import { environment } from '../environments/environment';
import { ApiResponse } from '../models/auth/auth';

const authConfig = (): AxiosRequestConfig => ({
    headers: {
        Authorization: `Bearer ${localStorage.getItem('auth_token')}`
    }
});

@Injectable({
    providedIn: 'root'
})
export class AxiosService {
    private baseUrl = environment.apiUrl;

    private buildUrl(url: string): string {
        if (/^https?:\/\//i.test(url)) {
            return url;
        }
        return `${this.baseUrl}${url}`;
    }

    private mergeConfig(config?: AxiosRequestConfig): AxiosRequestConfig {
        return { ...authConfig(), ...config };
    }

    private toApiResponse<T>(response: AxiosResponse<T>): ApiResponse<T> {
        return response.data as ApiResponse<T>;
    }

    get<T>(url: string, config?: AxiosRequestConfig): Observable<ApiResponse<T>> {
        return from(axios.get<T>(this.buildUrl(url), this.mergeConfig(config)))
            .pipe(map(this.toApiResponse));
    }

    post<T>(url: string, data?: any, config?: AxiosRequestConfig): Observable<ApiResponse<T>> {
        return from(axios.post<T>(this.buildUrl(url), data, this.mergeConfig(config)))
            .pipe(map(this.toApiResponse));
    }

    put<T>(url: string, data?: any, config?: AxiosRequestConfig): Observable<ApiResponse<T>> {
        return from(axios.put<T>(this.buildUrl(url), data, this.mergeConfig(config)))
            .pipe(map(this.toApiResponse));
    }

    delete<T>(url: string, config?: AxiosRequestConfig): Observable<ApiResponse<T>> {
        return from(axios.delete<T>(this.buildUrl(url), this.mergeConfig(config)))
            .pipe(map(this.toApiResponse));
    }
}