import { makeAutoObservable } from "mobx";
import { createContext } from "react";
import { LoginType, LoginErrorType, EMPTY_LOGIN, EMPTY_LOGIN_ERROR } from "./LoginPage.types";
import { postLogin } from "../../../api/AuthenticationApi";
import { userServiceStore } from "../../../services/userService/UserService.store";
import { serverErrorHandlingStore } from "../components/serverErrorHandling/ServerErrorHandling.store";

export class LoginPageStore {
    public loginData: LoginType = EMPTY_LOGIN;
    public errorData: LoginErrorType = EMPTY_LOGIN_ERROR;

    constructor() {
        makeAutoObservable(this);
    }

    public setEmailValue = (value: string) => this.loginData.email = value;

    public setPasswordValue = (value: string) => this.loginData.password = value;

    public setEmailError = (value: string) => this.errorData.emailError.message = value;
    
    public setPasswordError = (value: string) => this.errorData.passwordError.message = value;

    public setEmailIsTouched = () => this.errorData.emailError.isTouched = true;

    public setPasswordIsTouched = () => this.errorData.passwordError.isTouched = true;
    
    public reset = () => {
        this.loginData = EMPTY_LOGIN;
        this.errorData = EMPTY_LOGIN_ERROR;
    }

    public fetchUser = async () => {
        try {
            const user = await postLogin(this.loginData);
            userServiceStore.setUserData(user);
        } catch (error) {
            serverErrorHandlingStore.setServerErrorMessage(error);
        }
    }
}

export const loginPageStore = new LoginPageStore();
export const LoginPageContext = createContext(loginPageStore);