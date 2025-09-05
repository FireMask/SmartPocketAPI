export interface MovementViewModel {
    id: number;
    movementDate: string; 
    description: string;
    amount: number;
    userId: string;
    categoryId: number;
    paymentMethodId: number;
    recurringPaymentId?: number | null;
    movementTypeId: number;
    installmentCount?: number | null;
    installmentNumber?: number | null;
    creditCardPaymentId?: number | null;
    frequencyId?: number | null;
    isInstallment?: boolean;
}
