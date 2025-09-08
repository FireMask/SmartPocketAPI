import { Component, computed, Input, SimpleChanges, Signal } from '@angular/core';
import { Movement } from '../../../models/movements/movement';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-top-movements',
  imports: [
    CommonModule
  ],
  templateUrl: './top-movements.html',
  styles: ``
})
export class TopMovements {

  @Input() Movements: Movement[] = [];

  get sortedMovements(): Movement[] {
    return [...(this.Movements ?? [])].sort((a, b) => b.amount - a.amount);
  }

}
