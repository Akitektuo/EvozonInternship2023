import { observer } from "mobx-react";
import { useCallback, useContext, useEffect } from "react";
import { paginationStoreContext } from "./Pagination.store";
import { DOTS, usePagination } from "./UsePagination";
import classNames from "classnames";
import styles from "./Pagination.module.scss";
import { NavigateNext } from "@mui/icons-material";
import { NavigateBefore } from "@mui/icons-material";
import { useSearchParams } from "react-router-dom";
import { FIRST_PAGE_INDEX, MINIMUM_PAGINATION_RANGE } from "./PaginationConstants";

const PageQueryKey = "page";

interface Props {
	onChange: (page: number) => void;
}

export const PaginationComponent = observer(({ onChange }: Props) => {
	const [queryParams, setQueryParams] = useSearchParams();
    const { currentPage, setPage, reset } = useContext(paginationStoreContext);
	
    const paginationRange = usePagination();

	const setCurrentPage = useCallback((page: number) => {
		if (page === currentPage) {
			return;
		}

		setPage(page);
		setQueryParams(queryParams => {
			queryParams.set(PageQueryKey, page.toString());
			return queryParams;
		});
		onChange(page);
	}, [currentPage, onChange, setPage, setQueryParams]);

	useEffect(() => reset, [reset]);

	useEffect(() => {
		const pageAsString = queryParams.get(PageQueryKey);
		if (pageAsString) {
			setCurrentPage(parseInt(pageAsString));
		}
	}, [setCurrentPage, queryParams]);

    if (currentPage === 0 || paginationRange!.length < MINIMUM_PAGINATION_RANGE) {
      return null;
    }

    let lastPage = paginationRange![paginationRange!.length - 1];
    return (
		<ul className={styles.paginationContainer}>
			<li className={classNames(styles.paginationItem, {
				[styles.disabled]: currentPage === FIRST_PAGE_INDEX
			})}
				onClick={() => setCurrentPage(currentPage - 1)}
				key="previous">
					<NavigateBefore className={styles.navigateBefore} />
			</li>
			{paginationRange!.map((pageNumber, index) => {
			if (pageNumber === DOTS) {
				return <li className={classNames(styles.paginationItem, styles.dots)} key={index}>&#8230;</li>;
			}
  
			return (
				<li className={classNames(styles.paginationItem, {
					[styles.selected]: pageNumber === currentPage
				})}
					onClick={() => setCurrentPage(pageNumber as number)}
					key={index}>
				{pageNumber}
				</li>
			);
        	})}
			<li className={classNames(styles.paginationItem, {
				[styles.disabled]: currentPage === lastPage
			})}
				onClick={() => setCurrentPage(currentPage + 1)}
				key="next">
				<NavigateNext className={styles.navigateNext}/>
			</li>
      </ul>
    );
});