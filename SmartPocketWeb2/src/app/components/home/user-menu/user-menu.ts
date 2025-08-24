import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { AuthStore } from '../../../stores/AuthStore';

@Component({
  selector: 'app-user-menu',
  imports: [CommonModule],
  templateUrl: './user-menu.html',
  styles: ``
})
export class UserMenu {

  authStore = inject(AuthStore);

  showMenu: boolean = false;

}
