import { Injectable, signal, inject } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class HomeStore {

    private _isLoading = signal(false);
    private _isNewMovementModalOpen = signal(false);

    readonly select = {
        isLoading: this._isLoading.asReadonly(),
        isNewMovementModalOpen: this._isNewMovementModalOpen.asReadonly()
    };
    
    setLoading(isLoading: boolean) {
        this._isLoading.set(isLoading);
    }

    openNewMovementModal() {
        this._isNewMovementModalOpen.set(true);
    }

    closeNewMovementModal() {
        this._isNewMovementModalOpen.set(false);
    }

}
