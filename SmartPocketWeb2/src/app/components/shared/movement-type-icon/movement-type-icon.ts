import { Component, Input } from '@angular/core';
import { MovementType } from '../../../models/movements/MovementType';

@Component({
  selector: 'app-movement-type-icon',
  imports: [],
  templateUrl: './movement-type-icon.html',
  styles: ``
})
export class MovementTypeIcon {
  @Input() movementType: MovementType | null = null;
}
