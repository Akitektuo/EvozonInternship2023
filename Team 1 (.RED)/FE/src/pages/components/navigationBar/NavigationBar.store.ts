import { makeAutoObservable } from "mobx"; 
import { createContext } from "react";

export class NavigationBarStore {
    public isDrawerVisible = false;

    constructor() {
        makeAutoObservable(this);
    }

    public handleDrawerToggle = () => {
        this.isDrawerVisible = !this.isDrawerVisible;
    }
}

export const navigationBarStore = new NavigationBarStore();
export const NavigationBarContext = createContext(navigationBarStore);