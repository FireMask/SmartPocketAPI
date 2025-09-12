import { inject, Injectable } from '@angular/core';
import { from, Observable } from 'rxjs';
import { finalize, map } from 'rxjs/operators';
import axios, { AxiosRequestConfig, AxiosResponse } from 'axios';
import { environment } from '../environments/environment';
import { ApiResponse } from '../models/auth/api-response';
import { HomeStore } from '../stores/HomeStore';
import { AuthTokenStore } from '../stores/AuthTokenStore';


@Injectable({
    providedIn: 'root'
})
export class AxiosService {

    homeStore = inject(HomeStore);
    authTokenRef = inject(AuthTokenStore);

    authConfig = (): AxiosRequestConfig => ({
        headers: {
            Authorization: `Bearer ${ this.authTokenRef.token() }`
        }
    });

    private baseUrl = environment.apiUrl;

    private buildUrl(url: string): string {
        if (/^https?:\/\//i.test(url)) {
            return url;
        }
        return `${this.baseUrl}${url}`;
    }

    private mergeConfig(config?: AxiosRequestConfig): AxiosRequestConfig {
        return { ...this.authConfig(), ...config };
    }

    private toApiResponse<T>(response: AxiosResponse<T>): ApiResponse<T> {
        return response.data as ApiResponse<T>;
    }

    get<T>(url: string, config?: AxiosRequestConfig): Observable<ApiResponse<T>> {
        this.homeStore.setLoading(true);
        return from(axios.get<T>(this.buildUrl(url), this.mergeConfig(config)))
            .pipe(
                map(this.toApiResponse),
                finalize(() => this.homeStore.setLoading(false))
            );
    }

    post<T>(url: string, data?: any, config?: AxiosRequestConfig): Observable<ApiResponse<T>> {
        this.homeStore.setLoading(true);
        return from(axios.post<T>(this.buildUrl(url), data, this.mergeConfig(config)))
            .pipe(
                map(this.toApiResponse),
                finalize(() => this.homeStore.setLoading(false))
            );
    }

    put<T>(url: string, data?: any, config?: AxiosRequestConfig): Observable<ApiResponse<T>> {
        this.homeStore.setLoading(true);
        return from(axios.put<T>(this.buildUrl(url), data, this.mergeConfig(config)))
            .pipe(
                map(this.toApiResponse),
                finalize(() => this.homeStore.setLoading(false))
            );
    }

    delete<T>(url: string, config?: AxiosRequestConfig): Observable<ApiResponse<T>> {
        this.homeStore.setLoading(true);
        return from(axios.delete<T>(this.buildUrl(url), this.mergeConfig(config)))
            .pipe(
                map(this.toApiResponse),
                finalize(() => this.homeStore.setLoading(false))
            );
    }
}