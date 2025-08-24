import { HttpErrorResponse } from '@angular/common/http';

export function parseApiError(err: unknown): string {
    if (err instanceof HttpErrorResponse) {
        // Case 1: server responded with JSON
        if (typeof err.error === 'string') {
            return err.error; // sometimes backend sends plain text
        }

        if (err.error?.message) {
            return err.error.message; // convention: { message: "..." }
        }

        if (Array.isArray(err.error?.errors)) {
            return err.error.errors.join(', '); // validation array
        }

        // Fallback to status text
        return err.statusText || `HTTP ${err.status}`;
    }

    // Case 2: Error thrown manually or by RxJS
    if (err instanceof Error) {
        return err.message;
    }

    // Case 3: Unknown format
    return 'An unexpected error occurred. Please try again.';
}