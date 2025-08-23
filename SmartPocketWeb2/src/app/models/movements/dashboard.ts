import { Movement } from "./movement";

export interface DashboardData {
    top20Movements: Movement[];
    thisMonthMovementsCount: number;
    thisMonthSpent: number;
    thisMonthIncome: number;
    pendingMovementsRecurring: number;
    summaryPaymentMethods: CardMonthSummaryViewModel[];
}

export interface CardMonthSummaryViewModel {
    CardName: string;
    TotalSum: number;
    ThisPeriodAmount: number;
    PendingMovementsAmount: number;
    TransactionStartDate: Date;
    TransactionEndDate: Date;
    DueDate: Date;
}