import { makeAutoObservable } from "mobx";
import { createContext } from "react";
import { getMenusByCategory } from "../../api/MenuApi";
import { MenuListType, MenuPaginationOptionsType } from "./MenusByCategoryPage.types";
import { DEFAULT_PAGINATION_MODEL } from "../../shared/Pagination.types";
import { CategoriesEnum } from "../../shared/CategoriesEnum";

export class CategoryPageStore {
    public menusByCategory: MenuListType | null = null;

    public paginationMenuData: MenuPaginationOptionsType = DEFAULT_PAGINATION_MODEL;

    constructor() {
        makeAutoObservable(this);
    }

    private setMenus = (value: MenuListType) => this.menusByCategory = value;

    private setCategory = (value: number) => this.paginationMenuData.category = value;

    public getMenusByCategory = async (onError: () => void, category: string | undefined, page: number = 1) => {
        try {
            this.paginationMenuData.paginationModel.pageNumber = page;
            this.setCategory(Number(CategoriesEnum[category as keyof typeof CategoriesEnum]));
            const menus = await getMenusByCategory(this.paginationMenuData);
            this.setMenus(menus);
        } catch (error) {
            onError();
        }
    }
}

export const categoryPageStore = new CategoryPageStore();
export const CategoryPageContext = createContext(categoryPageStore);