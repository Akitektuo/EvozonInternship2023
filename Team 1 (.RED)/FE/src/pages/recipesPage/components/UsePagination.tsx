import { useContext, useMemo } from "react";
import { paginationStoreContext } from "./Pagination.store";
import { PAGES_WITHOUT_SIBLINGS, MINIMUM_PAGES_FOR_DOTS, NUMBER_OF_SIBLINGS, PAGES_IN_RANGE, FIRST_PAGE_INDEX } from "./PaginationConstants";

export const DOTS = '...';

const range = (start: number, end: number) => {
    let length = end - start + 1;
    return Array.from({ length }, (_, idx) => idx + start);
};

export const usePagination = () => {
    const { totalCount, pageSize, currentPage, siblingCount } = useContext(paginationStoreContext);
    
    const paginationRange = useMemo(() => {
      const totalPageCount = Math.ceil(totalCount / pageSize);

      const totalPageNumbers = siblingCount + PAGES_WITHOUT_SIBLINGS;
      
      if (totalPageNumbers >= totalPageCount) {
        return range(1, totalPageCount);
      }
  
      const leftSiblingIndex = Math.max(currentPage - siblingCount, FIRST_PAGE_INDEX);
      const rightSiblingIndex = Math.min(
        currentPage + siblingCount,
        totalPageCount
      );
  
      const shouldShowLeftDots = leftSiblingIndex > MINIMUM_PAGES_FOR_DOTS;
      const shouldShowRightDots = rightSiblingIndex < totalPageCount - MINIMUM_PAGES_FOR_DOTS;
  
      const lastPageIndex = totalPageCount;
  
      if (!shouldShowLeftDots && shouldShowRightDots) {
        let leftItemCount = PAGES_IN_RANGE + NUMBER_OF_SIBLINGS * siblingCount;
        let leftRange = range(FIRST_PAGE_INDEX, leftItemCount);
  
        return [...leftRange, DOTS, totalPageCount];
      }
  
      if (shouldShowLeftDots && !shouldShowRightDots) {
        let rightItemCount = PAGES_IN_RANGE + NUMBER_OF_SIBLINGS * siblingCount;
        let rightRange = range(
          totalPageCount - rightItemCount + 1,
          totalPageCount
        );
        return [FIRST_PAGE_INDEX, DOTS, ...rightRange];
      }
  
      if (shouldShowLeftDots && shouldShowRightDots) {
        let middleRange = range(leftSiblingIndex, rightSiblingIndex);
        return [FIRST_PAGE_INDEX, DOTS, ...middleRange, DOTS, lastPageIndex];
      }
      }, [totalCount, pageSize, siblingCount, currentPage]);
    
      return paginationRange;
};