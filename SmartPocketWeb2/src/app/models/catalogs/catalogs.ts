import { Category } from "../movements/Category";
import { Frequency } from "../movements/Frequency";
import { MovementType } from "../movements/MovementType";
import { PaymentMethod } from "../movements/PaymentMethod";

export interface Catalogs {
    categories: Category[];
    frequencies: Frequency[];
    paymentMethods: PaymentMethod[];
    movementTypes: MovementType[];
}