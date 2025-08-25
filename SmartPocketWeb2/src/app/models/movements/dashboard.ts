import { Movement } from "./Movement";

export interface DashboardData {
    top20Movements: Movement[];
    thisMonthMovementsCount: number;
    thisMonthSpent: number;
    thisMonthIncome: number;
    pendingMovementsRecurring: number;
    summaryPaymentMethods: CardMonthSummaryViewModel[];
}

export interface CardMonthSummaryViewModel {
    cardName: string;
    totalSum: number;
    thisPeriodAmount: number;
    pendingMovementsAmount: number;
    transactionStartDate: Date;
    transactionEndDate: Date;
    dueDate: Date;
}