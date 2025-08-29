import { Component, inject } from '@angular/core';
import { Card } from './card/card';
import { HomeStore } from '../../stores/HomeStore';
import { CurrencyPipe } from '@angular/common';
import { MovementStore } from '../../stores/MovementStore';
import { CardSummary } from './card-summary/card-summary';
import { TopMovements } from './top-movements/top-movements';
import { Movement } from '../../models/movements/Movement';
import { Timeline } from '../graphs/timeline/timeline';

@Component({
  selector: 'app-dashboard',
  imports: [
    Card,
    CardSummary,
    TopMovements,
    CurrencyPipe,
    Timeline
  ],
  templateUrl: './dashboard.html',
  styles: ``
})
export class Dashboard {

  homeStore = inject(HomeStore);
  movementStore = inject(MovementStore);

  dashboardData$ = this.movementStore.select.dashboardData;

  ngOnInit(): void {
    this.movementStore.getDashboardData();
  }

}
