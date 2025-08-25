import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AxiosService } from '../helpers/axios';
import { ApiResponse } from '../models/auth/ApiResponse';
import { Movement } from '../models/movements/Movement';
import { DashboardData } from '../models/movements/Dashboard';
import { MovementViewModel } from '../models/movements/MovementViewModel';

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

    createMovement(movement:Partial<MovementViewModel>): Observable<ApiResponse<Movement>> {
        return this.axiosInstance.post<Movement>("/movement", movement);
    }
}