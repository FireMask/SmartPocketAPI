import { User } from "../auth/user";
import { Category } from "./category";
import { MovementType } from "./movement-type";
import { PaymentMethod } from "./payment-method";
import { RecurringPayment } from "./recurring-payment";

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