import { NgClass } from '@angular/common';
import { Component, Input } from '@angular/core';
import { GButton } from '../../../models/props/g-button';

@Component({
  selector: 'app-group-button',
  imports: [NgClass],
  templateUrl: './group-button.html',
  styles: ``
})
export class GroupButton {

  @Input() Buttons: GButton[] = [];

  handleAction(button: GButton) {
    if (button.icon) {
      button.icon = button.icon.includes('rotate0')
        ? button.icon.replace(' rotate0', ' rotate180')
        : button.icon.replace(' rotate180', ' rotate0');
    }

    if (button.action) {
      button.action();
    }
  }
}
