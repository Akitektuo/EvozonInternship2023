import { BaseApi } from "./BaseApi";
import { UserCredentials, UserDetails, UserInformation } from "../shared/Types";

const API_PATH = "/api/user";

export const login = async (userCredentials: UserCredentials): Promise<UserDetails> => {
    const response = await BaseApi.post(`${API_PATH}/login`, userCredentials);
    return response.data;
}

export const register = async (userInformation: UserInformation) => {
    await BaseApi.post(`${API_PATH}/register`, userInformation);
}