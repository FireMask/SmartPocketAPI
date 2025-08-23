// app/store/ui-store.service.ts
import { Injectable, signal, computed, effect } from '@angular/core';

export type Theme = 'light' | 'dark';

@Injectable({ providedIn: 'root' })
export class HomeStore {
    // state
    private _count = signal(0);

    // selectors
    count = computed(() => this._count());

    // updaters
    inc(){
        this._count.update(c => c + 1);
    }
    dec(){
        this._count.update(c => c - 1);
    }
}
