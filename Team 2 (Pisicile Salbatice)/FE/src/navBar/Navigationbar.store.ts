import { makeAutoObservable } from "mobx";
import { createContext } from "react";

export class NavigationBarStore {
    public heightNavigationBar: number | null = null;

    constructor() {
        makeAutoObservable(this)
    }

    public setHeightNavigationbar = (value: number) => this.heightNavigationBar = value;
}

export const navigationBarStore = new NavigationBarStore();
export const NavigationBarContext = createContext(navigationBarStore);