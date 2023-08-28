import { BaseApi } from "./BaseApi";
import { LoginType, UserType } from "../pages/authenticationPages/loginPage/LoginPage.types";
import { RegisterUserType } from "../pages/authenticationPages/registerPage/RegisterPage.types";

const API_PATH = "api/user";

export const postLogin = async (loginData: LoginType): Promise<UserType> => {
    const { data } = await BaseApi.post(`${API_PATH}/login`, loginData);
    return data;
}

export const postRegister = async (registerData: RegisterUserType) => {
    await BaseApi.post(`${API_PATH}/register`, registerData);
}