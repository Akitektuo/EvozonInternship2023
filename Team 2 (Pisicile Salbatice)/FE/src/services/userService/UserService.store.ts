import { makeAutoObservable } from "mobx";
import { UserType } from "../../pages/authenticationPages/loginPage/LoginPage.types";
import { createContext } from "react";

export class UserServiceStore {
    public userData: UserType | null | undefined = undefined;

    constructor () {
        makeAutoObservable(this);
    }

    public isUserInitialized = () => this.userData !== undefined;

    public isAuthenticated = () => !!this.userData;

    public setUserData = (user: UserType) => {
        this.userData = user;
        sessionStorage.setItem("user", JSON.stringify(user));
    }

    public clearUserData = () => {
        this.userData = null;
        sessionStorage.removeItem("user");
    }

    public initialize = () => {
        const userDataAsString = sessionStorage.getItem("user");
        this.userData = userDataAsString && JSON.parse(userDataAsString);
    }
}

export const userServiceStore  = new UserServiceStore();
export const UserServiceContext = createContext(userServiceStore);