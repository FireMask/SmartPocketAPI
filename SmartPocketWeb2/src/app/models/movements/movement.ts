import { User } from "../auth/User";
import { Category } from "./Category";
import { MovementType } from "./MovementType";
import { PaymentMethod } from "./PaymentMethod";
import { RecurringPayment } from "./RecurringPayment";

export interface Movement {
    Id: number;
    MovementDate: Date;
    Description: string;
    Amount: number;
    InstallmentNumber?: number;
    UserId: string;
    User: User;
    CategoryId: number;
    Category: Category;
    PaymentMethodId: number;
    PaymentMethod: PaymentMethod;
    RecurringPaymentId?: number;
    RecurringPayment: RecurringPayment;
    MovementTypeId: number;
    MovementType: MovementType;
    CreditCardPaymentId?: number;
    CreditCardPayment: PaymentMethod;
    CreatedAt: Date;
    UpdatedAt: Date;
}