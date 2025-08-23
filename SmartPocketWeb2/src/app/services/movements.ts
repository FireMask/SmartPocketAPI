import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AxiosService } from '../helpers/axios';
import { ApiResponse } from '../models/auth/auth';
import { Movement } from '../models/movements/movement';
import { DashboardData } from '../models/movements/dashboard';

@Injectable({
    providedIn: 'root'
})
export class MovementService {

    axiosInstance: AxiosService = inject(AxiosService);

    getAllMovements(): Observable<ApiResponse<Movement>> {
        return this.axiosInstance.get<Movement>("/movements");
    }

    getDashboardData(): Observable<ApiResponse<DashboardData>> {
        return this.axiosInstance.get<DashboardData>("/dashboard");
    }
}