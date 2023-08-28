import { makeAutoObservable } from "mobx";
import { createContext } from "react";
import { EmptyUserInformation, UserInformation } from "../../shared/Types";
import { register } from "../../api/UserApi";
import { popUpNotificationStore } from "../../services/popUpNotifications/PopUpNotifications.store";

export class RegisterPageStore {
    public userInformation: UserInformation = EmptyUserInformation;
    public firstNameError = "";
    public lastNameError = "";
    public emailError = "";
    public passwordError = "";
    public confirmPasswordError = "";
    public isFirstNameTouched = false;
    public isLastNameTouched = false;
    public isEmailTouched = false;
    public isPasswordTouched = false;
    public isConfirmPasswordTouched = false;

    constructor() {
        makeAutoObservable(this);
    }

    public setFirstName = (firstName: string) => {
        this.userInformation.firstName = firstName;
        this.isFirstNameTouched = true;
    }

    public setFirstNameError = (errorMessage: string) => {
        this.firstNameError = errorMessage;
    }

    public setLastName = (lastName: string) => {
        this.userInformation.lastName = lastName;
        this.isLastNameTouched = true;
    }

    public setLastNameError = (errorMessage: string) => {
        this.lastNameError = errorMessage;
    }

    public setEmail = (email: string) => {
        this.userInformation.email = email;
        this.isEmailTouched = true;
    }

    public setEmailError = (errorMessage: string) => {
        this.emailError = errorMessage;
    }

    public setPassword = (password: string) => {
        this.userInformation.password = password;
        this.isPasswordTouched = true;
    }

    public setPasswordError = (errorMessage: string) => {
        this.passwordError = errorMessage;
    }

    public setConfirmPassword = (confirmPassword: string) => {
        this.userInformation.confirmPassword = confirmPassword;
        this.isConfirmPasswordTouched = true;
    }

    public setConfirmPasswordError = (errorMessage: string) => {
        this.confirmPasswordError = errorMessage;
    }

    public isRegisterFormValid = () => {
        if (this.firstNameError || this.lastNameError || this.emailError || this.passwordError || this.confirmPasswordError) {
            popUpNotificationStore.showError("Error", "Incorrect credentials");
            return false;
        }
        return true;
    }

    public registerUser = async (userInformation: UserInformation) => {
        if (!this.isRegisterFormValid()) {
            return false;
        }
        try {
            await register(userInformation);
            popUpNotificationStore.showSuccess("Register OK", `Registered as ${userInformation.firstName} ${userInformation.lastName}`);
            return true;
        } catch (error) {
            console.log(error);
            popUpNotificationStore.handleError(error);
            return false;
        }
    }

    public resetRegisterPage = () => {
        this.userInformation = EmptyUserInformation;
        this.firstNameError = "";
        this.lastNameError = "";
        this.emailError = "";
        this.passwordError = "";
        this.confirmPasswordError = "";
        this.isFirstNameTouched = false;
        this.isLastNameTouched = false;
        this.isEmailTouched = false;
        this.isPasswordTouched = false;
        this.isConfirmPasswordTouched = false;
    }
}

export const registerPageStore = new RegisterPageStore();
export const registerPageContext = createContext(registerPageStore);