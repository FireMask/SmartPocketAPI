export interface TableColumn {
    id: number;
    header: string;
    field: string;
    width?: string;
    cellTemplate?: any;
}

export interface TableRow {
    [key: string]: any;
}

export interface TableConfiguration {
    columns: TableColumn[];
    rows: TableRow[];
    totalRecords?: number;
    totalPages?: number;
    pageSize?: number;
    pageNumber?: number;
    loading?: boolean;
    showPaginator?: boolean;
    rowsPerPageOptions?: number[];
}
