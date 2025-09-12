import { Component, computed, Input, SimpleChanges, Signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MovementTypeIcon } from "../../shared/movement-type-icon/movement-type-icon";
import { Movement } from '../../../models/movements/movement';

@Component({
  selector: 'app-top-movements',
  imports: [
    CommonModule,
    MovementTypeIcon
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
