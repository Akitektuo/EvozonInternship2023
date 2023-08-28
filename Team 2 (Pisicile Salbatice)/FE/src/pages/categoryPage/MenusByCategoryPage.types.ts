import { PaginationOptionsType } from "../../shared/Pagination.types";

export interface MenuType {
    id?: number;
    category: string;
    name: string;
    price: number;
}

export interface MenuListType {
    menusList: MenuType[];
    totalMenusCount: number;
}

export interface MenuPaginationOptionsType extends PaginationOptionsType {
    category?: number;
}

export interface getMenusByCategoryType {
    onError: () => void; 
    category: string | undefined; 
    page?: number | undefined;
}