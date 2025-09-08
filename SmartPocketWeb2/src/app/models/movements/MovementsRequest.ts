export interface MovementsRequest {
    search?: string;
    categoryId?: number[];
    paymentMethodId?: number[];
    movementTypeId?: number[];
    startDate?: string;
    endDate?: string;
    pageNumber: number; //Default 1
    pageSize?: number; //Default 10
}
