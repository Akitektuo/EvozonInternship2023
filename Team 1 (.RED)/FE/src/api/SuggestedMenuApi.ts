import { getAuthorizationHeaders } from "../shared/Authorization";
import { SuggestedMenu, CategoryAndPrice } from "../shared/Types";
import { BaseApi } from "./BaseApi";

const API_PATH = "/api/menu";

export const getSuggestedMenu = async (categoryAndPrice: CategoryAndPrice): Promise<SuggestedMenu> => {
    const response = await BaseApi.post(`${API_PATH}/get-suggested-menu`, categoryAndPrice, getAuthorizationHeaders());
    const data = response.data;
    return data;
}

export const addSuggestedMenu = async (suggestedMenu: SuggestedMenu) => {
    await BaseApi.post(`${API_PATH}/add-suggested-menu`, suggestedMenu, getAuthorizationHeaders());
}