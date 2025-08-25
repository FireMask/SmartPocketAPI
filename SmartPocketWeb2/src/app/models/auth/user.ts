export interface User {
    id: string;
    email: string;
    password: string;
    name: string;
    verifyCode: string;
    isPremium: boolean;
    isAdmin: boolean;
}
