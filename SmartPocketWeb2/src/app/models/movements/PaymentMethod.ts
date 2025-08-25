import { User } from "../auth/User";


export interface PaymentMethod {
    id: number;
    userId: string;
    user: User;
    paymentMethodTypeId: number;
    paymentMethodType: number;
    name: string;
    bank: string;
    isCreditCard: boolean;
    dueDate: number;
    transactionDate: number;
    defaultInterestRate: number;
    isDefault: boolean;
    isActive: boolean;
}
