export interface User {
    id: string;
    email: string;
    password: string;
    name: string;
    verifyCode: string;
    isPremium: boolean;
    isAdmin: boolean;
}

export interface AuthToken {
    user: Partial<User>;
    token: string;
    expiresIn: number;
}