import { Category } from "../movements/category";
import { Frequency } from "../movements/frequency";
import { MovementType } from "../movements/movement-type";
import { PaymentMethod } from "../movements/payment-method";

export interface Catalogs {
    categories: Category[];
    frequencies: Frequency[];
    paymentMethods: PaymentMethod[];
    movementTypes: MovementType[];
}