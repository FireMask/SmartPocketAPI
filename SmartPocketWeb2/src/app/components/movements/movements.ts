import { Component, inject } from '@angular/core';
import { MovementStore } from '../../stores/MovementStore';
import { FormControl, FormGroup } from '@angular/forms';
import { MovementsRequest } from '../../models/movements/movements-request';
import { CurrencyPipe, DatePipe } from '@angular/common';
import { MovementTypeIcon } from "../shared/movement-type-icon/movement-type-icon";
import { TableLazyLoadEvent, TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';

@Component({
  selector: 'app-movements',
  imports: [CurrencyPipe, DatePipe, MovementTypeIcon, TableModule, ButtonModule],
  templateUrl: './movements.html',
  styles: ``
})
export class Movements {
  movementStore = inject(MovementStore);

  filters = new FormGroup({
    search: new FormControl(''),
    categoryId: new FormControl([]),
    paymentMethodId: new FormControl([]),
    movementTypeId: new FormControl([]),
    startDate: new FormControl(''),
    endDate: new FormControl(''),
    pageNumber: new FormControl(1),
    pageSize: new FormControl(10),
  });

  loadMovements() {
    const movementsRequest: MovementsRequest = {
      search: this.filters.get('search')?.value ?? '',
      categoryId: this.filters.get('categoryId')?.value ?? [],
      paymentMethodId: this.filters.get('paymentMethodId')?.value ?? [],
      movementTypeId: this.filters.get('movementTypeId')?.value ?? [],
      startDate: this.filters.get('startDate')?.value ?? '',
      endDate: this.filters.get('endDate')?.value ?? '',
      pageNumber: this.filters.get('pageNumber')?.value ?? 1,
      pageSize: this.filters.get('pageSize')?.value ?? 10,
    };
    this.movementStore.getMovementsData(movementsRequest);
  }

  ngOnInit(): void {
    this.loadMovements();
  }

  loadMovementsLazy(event: TableLazyLoadEvent) {
      this.filters.get('pageNumber')?.setValue((event.first ?? 0) / (event.rows ?? 10) + 1);
      this.filters.get('pageSize')?.setValue(event.rows ?? 10);
      this.loadMovements();
  }
}