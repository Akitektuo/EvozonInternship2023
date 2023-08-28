import { makeAutoObservable } from "mobx";
import { createContext } from "react";
import { UserDetails } from "../shared/Types";

const getLoggedUser = () => {
    const userString = localStorage.getItem("user");
    if (userString) {
        return JSON.parse(userString);
    }
}

export class UserDetailsStore {
    public userDetails?: UserDetails = getLoggedUser();
    
    constructor() {
        makeAutoObservable(this);
    }

    public isLoggedIn = () => !!this.userDetails;

    public isAdmin = () => {
        if (this.userDetails?.role !== "Admin") {
            return false;
        }
        return true;
    }

    public setUserDetails = (responseData: UserDetails) => {
        this.userDetails = responseData;
        localStorage.setItem("user", JSON.stringify(this.userDetails));
    }

    public clearUser = () => {
        if (!this.isLoggedIn) {
           return; 
        }
        localStorage.removeItem("user");
        this.userDetails = undefined;
    }
}

export const userDetailsStore = new UserDetailsStore();
export const userDetailsContext = createContext(userDetailsStore);