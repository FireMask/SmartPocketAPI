import { Component, inject, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Login } from '../login/login';
import { HomeStore } from '../../stores/HomeStore';
import { NewMovement } from '../movements/new-movement/new-movement';
import { Toast } from '../toast/toast';
import { AuthStore } from '../../stores/AuthStore';
import { UserMenu } from './user-menu/user-menu';
import { ConfigurationStore } from '../../stores/ConfigurationStore';
import { ConfigEnum } from '../../helpers/enums/config';
import { CommonModule } from '@angular/common';
import { GroupButton } from '../shared/group-button/group-button';
import { GButton, GButtonSize } from '../../models/props/GButton';

@Component({
  selector: 'home-root',
  imports: [
    RouterOutlet,
    UserMenu,
    Toast,
    Login,
    NewMovement,
    CommonModule,
    GroupButton
  ],
  templateUrl: './home.html'
})
export class Home {
  protected readonly title = signal('SmartPocket');

  homeStore = inject(HomeStore);
  authStore = inject(AuthStore);
  configStore = inject(ConfigurationStore);

  constructor() {
    console.log(this.configStore.select.configurations().find(c => c.key === ConfigEnum.DarkMode)?.value === 'true');
  }

  get isLoggedIn() {
    return this.authStore.select.isLoggedIn();
  }

  get isDarkMode() {
    return this.configStore.select.configurations().find(c => c.key === ConfigEnum.DarkMode)?.value === 'true';
  }

  buttonStyle = "border text-white font-semibold cursor-pointer border-none shadow-md"
    + " bg-rose-300/40 hover:bg-rose-400 text-gray-800 "
    + " dark:bg-gray-800 dark:hover:bg-gray-700 dark:text-gray-200";

  buttons: GButton[] = [
    {
      icon: 'fa-solid fa-plus',
      style: this.buttonStyle,
      buttonSize: GButtonSize.Large,
      action: () => this.homeStore.openNewMovementModal()
    },
    {
      icon: 'fa-solid fa-caret-down rotate0',
      style: this.buttonStyle,
      buttonSize: GButtonSize.Small,
      action: () => {
        console.log('Button 3 clicked');
      }
    }
  ];
}