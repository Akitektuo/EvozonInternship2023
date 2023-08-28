import { makeAutoObservable, runInAction } from "mobx";
import { Menu } from "../../../shared/Types";
import { getAllMenus } from "../../../api/MenuApi";
import { popUpNotificationStore } from "../../../services/popUpNotifications/PopUpNotifications.store";
import { createContext } from "react";

export class AllMenusStore {
    public menus: Menu[] = [];
    public isLoading = false;
    public errorMessage = "";
    public pageNumber = 1;
    public hasMore = true;

    constructor() {
        makeAutoObservable(this);
    }

    public getMenusByCategory = async (categoryId: number) => {
        this.isLoading = true;
        try {
            const responseData = await getAllMenus(this.pageNumber, categoryId);
            console.log(responseData);
            runInAction(() => {
                this.hasMore = responseData.length >= 10;
                this.menus = [...this.menus, ...responseData];
            });
        } catch (error) {
            popUpNotificationStore.handleError(error);
        } finally {
            runInAction(() => {
                this.isLoading = false;
                this.errorMessage = "";
            });
        }
    }

    public getNextMenus = async (categoryId: number) => {
        this.isLoading = true;
        try {
            const responseData = await getAllMenus(this.pageNumber, categoryId);
            console.log(responseData);
            runInAction(() => {
                this.hasMore = !(responseData.length < 10);
                this.menus = [...this.menus, ...responseData];
            });
        } catch (error) {
            popUpNotificationStore.handleError(error);
        } finally {
            runInAction(() => {
                this.isLoading = false;
                this.errorMessage = "";
            });
        }
    }

    public setNextPage = () => {
        this.pageNumber = this.pageNumber + 1;
    }

    public setErrorMessage = (errorMessage: string) => {
        this.errorMessage = errorMessage;
    }
    public resetPage = () => {
        this.pageNumber = 1;
        this.menus = [];
    }
}

export const allMenusStore = new AllMenusStore();
export const allMenusContext = createContext(allMenusStore);