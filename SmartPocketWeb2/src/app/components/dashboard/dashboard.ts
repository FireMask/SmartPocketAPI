import { Component, inject } from '@angular/core';
import { MovementService } from '../../services/movements';
import { Card } from './card/card';
import { DashboardData } from '../../models/movements/dashboard';

@Component({
  selector: 'app-dashboard',
  imports: [
    Card
  ],
  templateUrl: './dashboard.html',
  styles: ``
})
export class Dashboard {

  movementService = inject(MovementService);

  dashboardData: DashboardData | null = null;

  ngOnInit(): void {
    this.getDashboardData();
  }

  getAllMovements() {
    this.movementService.getAllMovements().subscribe({
      next: (response) => {
        console.log(response);
      },
      error: (error) => {
        console.error('Error fetching movements:', error);
      }
    });
  }

  getDashboardData() {
    this.movementService.getDashboardData().subscribe({
      next: (response) => {
        this.dashboardData = response.data;
        console.log(this.dashboardData);
        
      },
      error: (error) => {
        console.error('Error fetching dashboard data:', error);
      }
    });
  }

}
