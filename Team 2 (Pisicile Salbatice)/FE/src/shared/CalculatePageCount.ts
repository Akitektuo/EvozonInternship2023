export const calculatePageCount = (totalCounts: any, paginationType: any) => (totalCounts ? 
    Math.ceil(totalCounts / paginationType?.paginationModel?.pageSize) : 0);
