import { makeAutoObservable } from "mobx";
import { createContext } from "react";
import { UserCredentials, EmptyUserCredentials } from "../../shared/Types";
import { login } from "../../api/UserApi";
import { userDetailsStore } from "../../services/UserDetails.store";
import { popUpNotificationStore } from "../../services/popUpNotifications/PopUpNotifications.store";

export class LoginPageStore {
    public userCredentials: UserCredentials = EmptyUserCredentials;

    constructor() {
        makeAutoObservable(this);
    }

    public setEmail = (email: string) => {
        this.userCredentials.email = email;
    }

    public setPassword = (password: string) => {
        this.userCredentials.password = password;
    }

    public userAuthentication = async (userCredentials: UserCredentials) => {
        try {
            const responseData = await login(userCredentials);
            popUpNotificationStore.showSuccess("Login successful", `Logged as: ${responseData.email}`);
            userDetailsStore.setUserDetails(responseData);
            this.resetCredentials();
            return true;
        } catch (_) {
            popUpNotificationStore.showError("Login error", "Invalid credentials");
            return false;
        }
    }

    public resetCredentials = () => {
        this.userCredentials = EmptyUserCredentials;
    }
}

export const loginPageStore = new LoginPageStore();
export const loginPageContext = createContext(loginPageStore);