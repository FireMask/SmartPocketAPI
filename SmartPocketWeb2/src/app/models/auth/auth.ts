export interface ApiResponse<T> {
    success: boolean;
    data: T;
    error?: ApiError;
    message: string;
    code: string;
}

export interface ApiError {
    code: string;
    details: string;
    fields: { [key: string]: string };
}