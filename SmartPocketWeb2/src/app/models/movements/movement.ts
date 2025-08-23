import { User } from "../auth/user";

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

export interface Category {
    Id: number;
    Name: string;
    NameSpanish: string;
    Description: string;
    DescriptionSpanish: string;
    IsDefault: boolean;
    UserId: string;
    User: User;
}

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

export interface MovementType {
    Id: number;
    Name: string;
    NameSpanish: string;
}

export interface Frequency {
    Id: number;
    Name: string;
    NameSpanish: string;
}