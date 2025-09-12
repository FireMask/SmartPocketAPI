
export interface ApiError {
    code: string;
    details: string;
    fields: { [key: string]: string; };
}
