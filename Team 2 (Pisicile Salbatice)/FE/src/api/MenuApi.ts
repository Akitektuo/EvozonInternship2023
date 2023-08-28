import { MenuListType, MenuPaginationOptionsType } from "../pages/categoryPage/MenusByCategoryPage.types";
import { GeneratedMenuType } from "../pages/generatedMenuPage/GeneratedMenuPage.types";
import { CategoriesEnum } from "../shared/CategoriesEnum";
import { BaseApi, getAuthorizationHeader } from "./BaseApi";

const API_PATH = "api/menu";

export const getMenusByCategory = async (menusData: MenuPaginationOptionsType): Promise<MenuListType> => {
    const  { data } = await BaseApi.post(`${API_PATH}/get-all-menus`, menusData);
    return data;
}

export const getMenu = async (id: number) => {
    const { data } = await BaseApi.get(`${API_PATH}/get-menu/${id}`);
    return data;
}

export const getGeneratedMenu = async (menuType: CategoriesEnum, priceSuggestion: number): Promise<GeneratedMenuType> => {
    const { data } = await BaseApi.get(`${API_PATH}/get-generated-menu?menuType=${menuType}&priceSuggestion=${priceSuggestion}`, getAuthorizationHeader());
    return data;
}

export const postGeneratedMenu = (generatedMenu: GeneratedMenuType) =>
    BaseApi.post(`${API_PATH}/add-generated-menu`, generatedMenu, getAuthorizationHeader());