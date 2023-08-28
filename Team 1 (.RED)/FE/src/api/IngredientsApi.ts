import { BaseApi } from "./BaseApi";
import { getAuthorizationHeaders } from "../shared/Authorization";

const API_PATH = "/api/ingredient";

export const getAllIngredients = async () => {
    const response = await BaseApi.get(`${API_PATH}/get-all-ingredients`, getAuthorizationHeaders());
    return response.data;
}