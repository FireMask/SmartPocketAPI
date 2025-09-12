import { User } from "./user";

export interface AuthToken {
    user: Partial<User>;
    token: string;
    expiresIn: number;
}
