export interface CardMonthSummaryViewModel {
    cardName: string;
    totalSum: number;
    thisPeriodAmount: number;
    pendingMovementsAmount: number;
    transactionStartDate: Date;
    transactionEndDate: Date;
    dueDate: Date;
}
