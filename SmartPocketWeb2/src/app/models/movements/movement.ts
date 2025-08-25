import { User } from "../auth/User";
import { Category } from "./Category";
import { MovementType } from "./MovementType";
import { PaymentMethod } from "./PaymentMethod";
import { RecurringPayment } from "./RecurringPayment";

export interface Movement {
    id: number;
    movementDate: Date;
    description: string;
    amount: number;
    installmentNumber?: number;
    userId: string;
    user: User;
    categoryId: number;
    category: Category;
    paymentMethodId: number;
    paymentMethod: PaymentMethod;
    recurringPaymentId?: number;
    recurringPayment: RecurringPayment;
    movementTypeId: number;
    movementType: MovementType;
    creditCardPaymentId?: number;
    creditCardPayment: PaymentMethod;
    createdAt: Date;
    updatedAt: Date;
}