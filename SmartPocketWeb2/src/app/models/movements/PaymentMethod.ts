import { User } from "../auth/User";


export interface PaymentMethod {
    Id: number;
    UserId: string;
    User: User;
    PaymentMethodTypeId: number;
    PaymentMethodType: number;
    Name: string;
    Bank: string;
    IsCreditCard: boolean;
    DueDate: number;
    TransactionDate: number;
    DefaultInterestRate: number;
    IsDefault: boolean;
    IsActive: boolean;
}
