import { Injectable, signal, computed, inject } from '@angular/core';
import { MovementService } from '../services/movements';
import { MovementViewModel } from '../models/movements/MovementViewModel';
import { DashboardData } from '../models/movements/Dashboard';
import { NotificationsStore } from './NotificationStore';
import { parseApiError } from '../helpers/error';

@Injectable({ providedIn: 'root' })
export class MovementStore {
    private notify = inject(NotificationsStore);
    private movementService = inject(MovementService);
    
    private _dashboardData = signal<DashboardData>({} as DashboardData);

    readonly select = {
        dashboardData: this._dashboardData.asReadonly(),
    };

    getDashboardData() {
        this.movementService.getDashboardData().subscribe({
            next: (response) => {
                this._dashboardData.set(response.data);
                console.log(response.data);
                
            },
            error: (error) => {
                this.notify.error('Failed to load dashboard data. Please try again. ' + parseApiError(error));
            }
        });
    }

    createNewMovement(movementModel : Partial<MovementViewModel>) {
        this.movementService.createMovement(movementModel).subscribe({
            next: (response) => {   
                
            },
            error: (error) => {
                this.notify.error('Failed to create movement. Please try again. ' + parseApiError(error));
            }
        });
    }
}
