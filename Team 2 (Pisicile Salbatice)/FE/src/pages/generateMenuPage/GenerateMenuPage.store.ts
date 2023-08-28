import { makeAutoObservable } from "mobx";
import { CategoriesEnum } from "../../shared/CategoriesEnum";
import { createContext } from "react";

export class GenerateMenuPageStore {
    public menuType: CategoriesEnum = CategoriesEnum.Fitness; 
    public priceSuggestion: number = NaN;

    constructor () {
        makeAutoObservable(this);
    }

    public setMenuType = (value: CategoriesEnum) => this.menuType = value;

    public setPriceSuggestion = (value: number) => this.priceSuggestion = value;
}

export const generateMenuPageStore = new GenerateMenuPageStore();
export const GenerateMenuPageContext = createContext(generateMenuPageStore);