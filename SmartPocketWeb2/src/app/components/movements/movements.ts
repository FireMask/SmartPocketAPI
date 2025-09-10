import { Component, inject } from '@angular/core';
import { MovementStore } from '../../stores/MovementStore';
import { FormControl, FormGroup } from '@angular/forms';
import { MovementsRequest } from '../../models/movements/MovementsRequest';
import { CurrencyPipe, DatePipe } from '@angular/common';
import { MovementTypeIcon } from "../shared/movement-type-icon/movement-type-icon";

@Component({
  selector: 'app-movements',
  imports: [CurrencyPipe, DatePipe, MovementTypeIcon],
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

  ngOnInit(): void {
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
}