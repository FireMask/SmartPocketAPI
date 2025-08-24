import { Component, inject } from '@angular/core';
import { Card } from './card/card';
import { HomeStore } from '../../stores/HomeStore';
import { CurrencyPipe } from '@angular/common';
import { MovementStore } from '../../stores/MovementStore';

@Component({
  selector: 'app-dashboard',
  imports: [
    Card,
    CurrencyPipe
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
