import { Component, Input, Output } from '@angular/core';

@Component({
  selector: 'card-component',
  imports: [],
  templateUrl: './card.html',
  styles: ``
})
export class Card {

  @Input() title: string = 'Card Title';
  @Input() content: string = "0";
  @Input() icon: string = 'fas fa-wallet';
  @Input() icon_color: string = 'text-indigo-500';
  @Input() isMoney: boolean = true;

}
