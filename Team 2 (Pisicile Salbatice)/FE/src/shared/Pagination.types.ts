export const DEFAULT_PAGINATION_MODEL: PaginationOptionsType  = {
    paginationModel: {
        pageNumber: 1,
        pageSize: 12
    }
};

export interface PaginationOptionsType {
    paginationModel: PageDetailsType;
}

export interface PageDetailsType {
    pageSize: number;
    pageNumber: number;
}