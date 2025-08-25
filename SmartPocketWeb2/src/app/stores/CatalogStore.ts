import { Injectable, signal, computed, inject } from '@angular/core';
import { NotificationsStore } from './NotificationStore';
import { parseApiError } from '../helpers/error';
import { CatalogsService } from '../services/catalogs';
import { Catalogs } from '../models/catalogs/catalogs';
import { Category } from '../models/movements/Category';

@Injectable({ providedIn: 'root' })
export class CatalogStore {
    private notify = inject(NotificationsStore);
    private catalogService = inject(CatalogsService);

    private _catalogs = signal<Catalogs | null>(null);
    private _topCategories = signal<Partial<Category>[]>([]);

    constructor() {
        this.getCatalogs();
    }

    readonly select = {
        categories: computed(() => this._catalogs()?.categories ?? []),
        frequencies: computed(() => this._catalogs()?.frequencies ?? []),
        paymentMethods: computed(() => this._catalogs()?.paymentMethods ?? []),
        movementTypes: computed(() => this._catalogs()?.movementTypes ?? []),
        topCategories: this._topCategories.asReadonly()
    };

    getCatalogs() {
        this.catalogService.getCatalogs().subscribe({
            next: (response) => {
                this._catalogs.set(response.data);
            },
            error: (error) => {
                this.notify.error('Failed to load catalogs.' + parseApiError(error));
            }
        });
        this.catalogService.getTopCategories().subscribe({
            next: (response) => {
                this._topCategories.set(response.data);
            },
            error: (error) => {
                this.notify.error('Failed to load top categories.' + parseApiError(error));
            }
        });
    }
}
