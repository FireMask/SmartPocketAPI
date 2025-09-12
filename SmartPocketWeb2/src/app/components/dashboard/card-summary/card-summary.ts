import { Component, Input } from '@angular/core';
import { CardMonthSummaryViewModel } from '../../../models/movements/card-month-summary-view-model';
import { CurrencyPipe, DatePipe } from '@angular/common';

@Component({
  selector: 'app-card-summary',
  imports: [ DatePipe, CurrencyPipe],
  templateUrl: './card-summary.html',
  styles: ``
})
export class CardSummary {

  @Input() PaymentMethodSummary: CardMonthSummaryViewModel = {
    cardName: 'Card Name',
    totalSum: 0,
    thisPeriodAmount: 200000,
    pendingMovementsAmount: 0,
    transactionStartDate: new Date(),
    transactionEndDate: new Date(),
    dueDate: new Date()
  }

}
