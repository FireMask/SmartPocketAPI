import { User } from "../auth/user";
import { Frequency } from "./frequency";
import { Category } from "./category";
import { PaymentMethod } from "./payment-method";
import { MovementType } from "./movement-type";


export interface RecurringPayment {
    id: number;
    description: string;
    isInterestFreePayment: boolean;
    installmentCount: number;
    nextInstallmentCount: number;
    installmentAmount: number;
    installmentAmountPerPeriod: number;
    startDate: Date;
    endDate?: Date;
    nextInstallmentDate: Date;
    lastInstallmentDate?: Date;
    isActive: boolean;
    categoryId: number;
    category: Category;
    paymentMethodId: number;
    paymentMethod: PaymentMethod;
    movementTypeId: number;
    movementType: MovementType;
    creditCardPaymentId?: number;
    creditCardPayment: PaymentMethod;
    userId: string;
    user: User;
    frequencyId: number;
    frequency: Frequency;
    createdAt: Date;
    updatedAt: Date;
}
