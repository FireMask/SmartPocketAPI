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

@Component({
  selector: 'home-root',
  imports: [
    RouterOutlet,
    UserMenu,
    Toast,
    Login,
    NewMovement,
    CommonModule
],
  templateUrl: './home.html'
})
export class Home {
  protected readonly title = signal('SmartPocket');

  homeStore = inject(HomeStore);
  authStore = inject(AuthStore);
  configStore = inject(ConfigurationStore);

  constructor(){
    console.log(this.isDarkMode);
    console.log(this.configStore.select.configurations().find(c => c.key === ConfigEnum.DarkMode)?.value === 'true');
  }

  get isLoggedIn() {
    return this.authStore.select.isLoggedIn();
  }

  get isDarkMode() {
    return this.configStore.select.configurations().find(c => c.key === ConfigEnum.DarkMode)?.value === 'true';
  }
}
