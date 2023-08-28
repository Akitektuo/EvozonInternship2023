import { makeAutoObservable, runInAction } from "mobx";
import { createContext } from "react";
import { MenuType } from "./MenuPage.types";
import { getMenu } from "../../api/MenuApi";

export class MenuPageStore {
    public menuData: MenuType | null = null;

    constructor() {
        makeAutoObservable(this);
    }

    public fetchMenu = async (id: number, onError: () => void) => {
        try {
            const menuData = await getMenu(id);
            runInAction(() => this.menuData = menuData);
        } catch (error) {
            onError();
        }
    }
}

export const menuPageStore = new MenuPageStore();
export const MenuPageContext = createContext(menuPageStore);