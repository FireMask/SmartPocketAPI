import { User } from "../auth/User";


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
