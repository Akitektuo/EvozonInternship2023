import { makeAutoObservable } from "mobx";
import { RegisterErrorType, RegisterUserType, EMPTY_REGISTER_USER, EMPTY_REGISTER_ERROR } from "./RegisterPage.types";
import { createContext } from "react";
import { postRegister } from "../../../api/AuthenticationApi";
import { serverErrorHandlingStore } from "../components/serverErrorHandling/ServerErrorHandling.store";

export class RegisterPageStore {
    public registerData: RegisterUserType = EMPTY_REGISTER_USER;
    public errorData: RegisterErrorType = EMPTY_REGISTER_ERROR;

    constructor() {
        makeAutoObservable(this);
    }

    public setFirstNameValue = (value: string) => this.registerData.firstName = value;

    public setLastNameValue = (value: string) => this.registerData.lastName = value;

    public setEmailValue = (value: string) => this.registerData.email = value;

    public setPasswordValue = (value: string) => this.registerData.password = value;

    public setConfirmedPasswordValue = (value: string) => this.registerData.confirmedPassword = value;

    public setFirstNameError = (value: string) => this.errorData.firstNameError.message = value;

    public setLastNameError = (value: string) => this.errorData.lastNameError.message = value;

    public setEmailError = (value: string) => this.errorData.emailError.message = value;

    public setPasswordError = (value: string) => this.errorData.passwordError.message = value;

    public setConfirmedPasswordError = (value: string) => this.errorData.confirmedPasswordError.message = value;

    public setFirstNameIsTouched = () => this.errorData.firstNameError.isTouched = true;

    public setLastNameIsTouched = () => this.errorData.lastNameError.isTouched = true;

    public setEmailIsTouched = () => this.errorData.emailError.isTouched = true;

    public setPasswordIsTouched = () => this.errorData.passwordError.isTouched = true;

    public setConfirmedPasswordIsTouched = () => this.errorData.confirmedPasswordError.isTouched = true;

    public reset = () => {
        this.registerData = EMPTY_REGISTER_USER;
        this.errorData = EMPTY_REGISTER_ERROR;
    }

    public createUser = async () => {
        try {
            await postRegister(this.registerData);
            return true;
        } catch (error: any) {
            serverErrorHandlingStore.setServerErrorMessage(error);
        }
    }
}

export const registerPageStore = new RegisterPageStore();
export const RegisterPageContext = createContext(registerPageStore);