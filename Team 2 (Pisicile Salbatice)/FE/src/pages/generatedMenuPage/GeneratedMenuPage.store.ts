import { makeAutoObservable, runInAction } from "mobx"
import { GeneratedMenuType } from "./GeneratedMenuPage.types";
import { CategoriesEnum } from "../../shared/CategoriesEnum";
import { createContext } from "react";
import { serverErrorHandlingStore } from "../authenticationPages/components/serverErrorHandling/ServerErrorHandling.store";
import { MenuType } from "../menuPage/MenuPage.types";
import { getGeneratedMenu, postGeneratedMenu } from "../../api/MenuApi";

export class GeneratedMenuPageStore {
    public generatedMenu: GeneratedMenuType | null = null;
    public displayMenu: MenuType | null = null;
    public isLoading: boolean = true;

    constructor() {
        makeAutoObservable(this);
    }

    public fetchMenu = async (menuType: CategoriesEnum, priceSuggestion: number, onError: () => void) => {
        try {
            const generatedMenu = await getGeneratedMenu(menuType, priceSuggestion);
            const displayMenu = this.mapMenu(menuType, generatedMenu);
            runInAction(() => {
                this.generatedMenu = generatedMenu;
                this.displayMenu = displayMenu;
                this.isLoading = false;
            });
        } catch (error) {
            runInAction(() => this.isLoading = false);
            serverErrorHandlingStore.setServerErrorMessage(error);
            onError();
        }
    }

    public postMenu = async (onSuccess: () => void) => {
        if (!this.generatedMenu) {
            return;
        }

        try {
            this.isLoading = true;
            await postGeneratedMenu(this.generatedMenu);
            onSuccess();
        } catch (error) {
            runInAction(() => this.isLoading = false);
            serverErrorHandlingStore.setServerErrorMessage(error);
        }
    }

    public reset = () => {
        this.generatedMenu = null;
        this.displayMenu = null;
        this.isLoading = true;
    }

    private mapMenu = (menuType: CategoriesEnum, generatedMenu: GeneratedMenuType): MenuType => ({
        id: 0,
        category: menuType,
        name: generatedMenu.name,
        price: generatedMenu.meals.sumBy(meal => meal.price),
        meals: generatedMenu.meals.map((meal, index) => ({
            id: index,
            name: meal.name,
            description: meal.description,
            price: meal.price,
            mealTypeId: meal.mealTypeId,
            ingredients: meal.recipe.ingredients
        }))
    });
}

export const generatedMenuPageStore = new GeneratedMenuPageStore();
export const GeneratedMenuPageContext = createContext(generatedMenuPageStore);