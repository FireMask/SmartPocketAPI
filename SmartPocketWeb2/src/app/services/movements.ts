import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AxiosService } from '../helpers/axios';
import { ApiResponse } from '../models/auth/ApiResponse';
import { Movement } from '../models/movements/movement';
import { DashboardData } from '../models/movements/dashboard';
import { MovementViewModel } from '../models/movements/movementViewModel';
import { MovementsRequest } from '../models/movements/MovementsRequest';
import { PagedResult } from '../models/apiResults/PagedResult';

@Injectable({
    providedIn: 'root'
})
export class MovementService {

    axiosInstance: AxiosService = inject(AxiosService);

    getAllMovements(filters:MovementsRequest): Observable<ApiResponse<PagedResult<Movement>>> {
        return this.axiosInstance.get<PagedResult<Movement>>("/movements",{
            params: filters,
            paramsSerializer: {
                indexes: null, // no brackets at all
            }
        });
    }

    getDashboardData(): Observable<ApiResponse<DashboardData>> {
        return this.axiosInstance.get<DashboardData>("/dashboard");
    }

    createMovement(movement:Partial<MovementViewModel>): Observable<ApiResponse<Movement>> {
        return this.axiosInstance.post<Movement>("/movement", movement);
    }
}