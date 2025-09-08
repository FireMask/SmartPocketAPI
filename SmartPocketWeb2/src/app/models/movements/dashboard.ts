import { CardMonthSummaryViewModel } from "./CardMonthSummaryViewModel";
import { Movement } from "./movement";

export interface DashboardData {
    top20Movements: Movement[];
    thisMonthMovementsCount: number;
    thisMonthSpent: number;
    thisMonthIncome: number;
    pendingMovementsRecurring: number;
    summaryPaymentMethods: CardMonthSummaryViewModel[];
}