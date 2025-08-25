import { User } from "../auth/User";


export interface Category {
    id: number;
    name: string;
    nameSpanish: string;
    description: string;
    descriptionSpanish: string;
    isDefault: boolean;
    userId: string;
    user: User;
}
