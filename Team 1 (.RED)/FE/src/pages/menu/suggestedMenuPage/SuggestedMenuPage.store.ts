import { makeAutoObservable, runInAction, toJS } from "mobx";
import { CategoryAndPrice, MenuCategory, SuggestedMenu, emptyCategoryAndPrice } from "../../../shared/Types";
import { addSuggestedMenu, getSuggestedMenu } from "../../../api/SuggestedMenuApi";
import { createContext } from "react";
import { popUpNotificationStore } from "../../../services/popUpNotifications/PopUpNotifications.store";

export class GetSuggestedMenuStore {
    public suggestedMenu: SuggestedMenu | null = null;
    public categoryAndPrice: CategoryAndPrice = emptyCategoryAndPrice;
    public isLoading = false;

    constructor() {
        makeAutoObservable(this);
    }

    public getSuggestedMenu = async () => {
        this.isLoading = true;
        try {
            const data = await getSuggestedMenu(this.categoryAndPrice)
            runInAction(() => {
                this.suggestedMenu = data;
            })
            console.log(toJS(this.suggestedMenu));
        } catch (error) {
            popUpNotificationStore.handleError(error);
        } finally {
            runInAction(() => {
                this.isLoading = false;
            })
        }
    }

    public saveSuggestedMenu = async () => {
        try {
            if(this.suggestedMenu)
                await addSuggestedMenu(this.suggestedMenu);
                popUpNotificationStore.showSuccess("Ok","Menu Added Successfully");
        } catch (error) {
            popUpNotificationStore.handleError(error);
        }
    }

    public setPriceSuggestion = (value: string) => {
        this.categoryAndPrice.priceSuggestion = +value;
    }

    public setCategory = (value: MenuCategory) => {
        this.categoryAndPrice.categoryId = value;
    }
}

export const getSuggestedMenuStore = new GetSuggestedMenuStore();
export const getSuggestedMenuContext = createContext(getSuggestedMenuStore);