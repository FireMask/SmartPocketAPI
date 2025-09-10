import { Injectable, signal, computed, inject } from '@angular/core';
import { MovementService } from '../services/movements';
import { MovementViewModel } from '../models/movements/movementViewModel';
import { DashboardData } from '../models/movements/dashboard';
import { NotificationsStore } from './NotificationStore';
import { parseApiError } from '../helpers/error';
import { Movement } from '../models/movements/movement';
import { MovementsRequest } from '../models/movements/MovementsRequest';
import { PagedResult } from '../models/apiResults/PagedResult';

@Injectable({ providedIn: 'root' })
export class MovementStore {
    private notify = inject(NotificationsStore);
    private movementService = inject(MovementService);
    
    private _dashboardData = signal<DashboardData>({} as DashboardData);
    private _movements = signal<PagedResult<Movement>>({
        items: [],
        totalCount: 0,
        pageSize: 10,
        pageNumber: 1,
        totalPages: 0
    });
    private _isLoading = signal<boolean>(false);

    readonly select = {
        dashboardData: this._dashboardData.asReadonly(),
        movements: this._movements.asReadonly(),
        isLoading: this._isLoading.asReadonly()
    };

    getDashboardData() {
        this.movementService.getDashboardData().subscribe({
            next: (response) => {
                this._dashboardData.set(response.data);
            },
            error: (error) => {
                this.notify.error('Failed to load dashboard data. Please try again. ' + parseApiError(error));
            }
        });
    }

    getMovementsData(filters: MovementsRequest) {
        this._isLoading.set(true);
        this.movementService.getAllMovements(filters).subscribe({
            next: (response) => {
                this._movements.set(response.data);
            },
            error: (error) => {
                this.notify.error('Failed to load movements. Please try again. ' + parseApiError(error));
            },
            complete: () => this._isLoading.set(false)
        });
    }

    createNewMovement(movementModel : Partial<MovementViewModel>) {
        this.movementService.createMovement(movementModel).subscribe({
            next: (response) => {   
                this.notify.success('Movement created');
                this.getDashboardData();
            },
            error: (error) => {
                this.notify.error('Failed to create movement. Please try again. ' + parseApiError(error));
            }
        });
    }
}
