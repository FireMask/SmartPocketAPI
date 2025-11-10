import { Injectable, signal, inject } from '@angular/core';
import { Movement } from '../models/movements/movement';

@Injectable({ providedIn: 'root' })
export class HomeStore {

    private _isLoading = signal(false);
    private _isNewMovementModalOpen = signal(false);
    private _editMovement = signal<Movement | null>(null);

    readonly select = {
        isLoading: this._isLoading.asReadonly(),
        isNewMovementModalOpen: this._isNewMovementModalOpen.asReadonly(),
        editMovement: this._editMovement.asReadonly()
    };
    
    setLoading(isLoading: boolean) {
        this._isLoading.set(isLoading);
    }

    openNewMovementModal() {
        this._isNewMovementModalOpen.set(true);
    }

    openEditMovementModal(movement : Movement) {
        this._editMovement.set(movement);
        this._isNewMovementModalOpen.set(true);
    }

    closeNewMovementModal() {
        this._editMovement.set(null);
        this._isNewMovementModalOpen.set(false);
    }

}
