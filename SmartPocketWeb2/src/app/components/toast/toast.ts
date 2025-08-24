import { Component, inject } from '@angular/core';
import { NotificationsStore } from '../../stores/NotificationStore';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-toast',
  imports: [
    CommonModule
  ],
  templateUrl: './toast.html',
  styles: ``
})
export class Toast {
  store = inject(NotificationsStore);
}
