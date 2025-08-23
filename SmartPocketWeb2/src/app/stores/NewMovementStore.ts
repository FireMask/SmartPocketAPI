import { Injectable, signal, computed, inject } from '@angular/core';
import { MovementService } from '../services/movements';
import { MovementViewModel } from '../models/movements/movementViewModel';

@Injectable({ providedIn: 'root' })
export class NewMovementStore {
    movementService = inject(MovementService);

    private _isNewMovementModalOpen = signal(false);
    isNewMovementModalOpen = computed(() => this._isNewMovementModalOpen());

    open() {
        this._isNewMovementModalOpen.set(true);
    }

    close() {
        this._isNewMovementModalOpen.set(false);
    }

    save(movementModel : Partial<MovementViewModel>) {
        this.movementService.createMovement(movementModel).subscribe({
            next: (response) => {   
                console.log('Movement created:', response);
            },
            error: (error) => {
                console.error('Error creating movement:', error);
            }
        });
        this.close();
    }
}
