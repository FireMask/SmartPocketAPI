// app/store/ui-store.service.ts
import { Injectable, signal, computed } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class HomeStore {
    private _isLoading = signal(false);

    isLoading = computed(() => this._isLoading());

    setLoading(isLoading: boolean) {
        this._isLoading.set(isLoading);
    }
}
