import { User } from "../auth/User";
import { Frequency } from "./Frequency";
import { Category } from "./Category";
import { PaymentMethod } from "./PaymentMethod";
import { MovementType } from "./MovementType";


export interface RecurringPayment {
    Id: number;
    Description: string;
    IsInterestFreePayment: boolean;
    InstallmentCount: number;
    NextInstallmentCount: number;
    InstallmentAmount: number;
    InstallmentAmountPerPeriod: number;
    StartDate: Date;
    EndDate?: Date;
    NextInstallmentDate: Date;
    LastInstallmentDate?: Date;
    IsActive: boolean;
    CategoryId: number;
    Category: Category;
    PaymentMethodId: number;
    PaymentMethod: PaymentMethod;
    MovementTypeId: number;
    MovementType: MovementType;
    CreditCardPaymentId?: number;
    CreditCardPayment: PaymentMethod;
    UserId: string;
    User: User;
    FrequencyId: number;
    Frequency: Frequency;
    CreatedAt: Date;
    UpdatedAt: Date;
}
