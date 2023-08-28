import { makeAutoObservable } from "mobx";
import { createContext } from "react";
import { PaginationInformation } from "../../../shared/Types";

export class PaginationStore {
    public totalCount: number = 0;
    public pageSize: number = 10;
    public siblingCount: number = 1;
    public currentPage: number = 1;

    constructor() {
        makeAutoObservable(this);
    }

    public setPage = (page: number) => {
        this.currentPage = page;
    }

    public setTotalCount = (totalCount: number) => {
        this.totalCount = totalCount;
    }

    public getInformation = (): PaginationInformation => {
        return { pageNumber: this.currentPage, pageSize: this.pageSize }
    }

    public reset = () => {
        this.currentPage = 1;
        this.pageSize = 10;
        this.siblingCount = 1;
        this.totalCount = 0;
    }
}

export const paginationStore = new PaginationStore();
export const paginationStoreContext = createContext(paginationStore);
