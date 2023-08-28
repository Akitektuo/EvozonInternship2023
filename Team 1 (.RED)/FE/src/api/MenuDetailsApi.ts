import { BaseApi } from "./BaseApi";
import { Menu, MenuDetails } from "../shared/Types";
import { getAuthorizationHeaders } from "../shared/Authorization";

const API_PATH = "/api/menu";

export const getMenuDetails = async (menuId: string): Promise<MenuDetails> => {
    const response = await BaseApi.get(`${API_PATH}/get-menu/${menuId}`, getAuthorizationHeaders());
    const data = response.data;
    return data;
}