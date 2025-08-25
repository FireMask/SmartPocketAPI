import { User } from "./User";

export interface AuthToken {
    user: Partial<User>;
    token: string;
    expiresIn: number;
}
