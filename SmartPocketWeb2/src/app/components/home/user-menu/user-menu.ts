import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { AuthStore } from '../../../stores/AuthStore';
import { ConfigurationStore } from '../../../stores/ConfigurationStore';
import { ConfigEnum } from '../../../helpers/enums/config';

@Component({
  selector: 'app-user-menu',
  imports: [CommonModule],
  templateUrl: './user-menu.html',
  styles: ``
})
export class UserMenu {

  authStore = inject(AuthStore);
  configurationStore = inject(ConfigurationStore);

  showMenu: boolean = false;

  toggleDarkMode() {
    const newValue = !this.configurationStore.isDarkMode;
    this.configurationStore.setConfiguration(ConfigEnum.DarkMode, newValue.toString());
  }

  onOutsideClick(event: MouseEvent) {
    const target = event.target as HTMLElement;
    const userMenuDropdown = document.getElementById('userMenuDropdown');
    const userMenuButton = document.getElementById('userMenuButton');

    if (userMenuDropdown && userMenuButton) {
      if (!userMenuDropdown.contains(target) && !userMenuButton.contains(target)) {
        this.showMenu = false;
      }
    }
  }

}
