export interface MovementViewModel {
    id: number;
    movementDate: Date; 
    description: string;
    amount: number;
    userId: string;
    categoryId: number;
    paymentMethodId: number;
    recurringPaymentId?: number | null;
    movementTypeId: number;
    installmentNumber?: number | null;
    creditCardPaymentId?: number | null;
}
