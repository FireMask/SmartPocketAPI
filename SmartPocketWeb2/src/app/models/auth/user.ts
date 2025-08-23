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
    token: string;
    expiresIn: number;
}