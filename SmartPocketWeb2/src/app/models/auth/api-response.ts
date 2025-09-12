import { ApiError } from "./api-error";

export interface ApiResponse<T> {
    success: boolean;
    data: T;
    error?: ApiError;
    message: string;
    code: string;
}