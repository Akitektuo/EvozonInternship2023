import { getAuthorizationHeaders } from "../shared/Authorization";
import { CategoryInformation, Menu } from "../shared/Types";
import { BaseApi } from "./BaseApi";

const API_PATH = "/api/menu";

export const getAllMenus = async (pageNumber: number, categoryId: number): Promise<Menu[]> => {
    const categoryInformation: CategoryInformation = {
        pageNumber: pageNumber,
        pageSize: 10,
        categoryId: categoryId
    }
    const response = await BaseApi.post(`${API_PATH}/get-all-menus`, categoryInformation, getAuthorizationHeaders());
    const data = response.data.items;
    return data;
}