import { makeAutoObservable, runInAction } from "mobx";
import { createContext } from "react";
import { MenuDetails } from "../../../shared/Types";
import { getMenuDetails } from "../../../api/MenuDetailsApi";
import { popUpNotificationStore } from "../../../services/popUpNotifications/PopUpNotifications.store";

export class MenuDetailsStore {
    public menu: MenuDetails | null = null;
    public isLoading = false;

    constructor() {
        makeAutoObservable(this);
    }

    public getMenuDetailsById = async (menuId: string) => {
        this.isLoading = true;
        try {
            const responseData = await getMenuDetails(menuId);
            runInAction(() => {
                this.menu = responseData;
            })
        } catch (error) {
            popUpNotificationStore.handleError(error);
        } finally {
            runInAction(() => {
                this.isLoading = false;
            })
        }
    }

    public resetMenu() {
        this.menu = null;
    }
}

export const menuDetailsStore = new MenuDetailsStore();
export const menuDetailsContext = createContext(menuDetailsStore);