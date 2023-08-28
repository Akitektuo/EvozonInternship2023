import { isAxiosError } from "axios";
import { makeAutoObservable } from "mobx";
import { createContext } from "react";
import { ServerErrorStatus, ServerErrosTypes } from "./ServerError.types";

export class ServerErrorHandling {
    public serverErrorMessage: string = "";
    public serverErrors: ServerErrosTypes = {};
    public isPopUpOpened: boolean = false;

    constructor() {
        makeAutoObservable(this);
    }

    public setPopUpIsOpened = (value: boolean) => this.isPopUpOpened = value;

    public setServerErrorMessage = (error: any) => {
        if (!isAxiosError(error)) {
            return;
        } 
        if (error.response?.status === ServerErrorStatus.BadRequest) {
            this.serverErrorMessage = error.response?.data?.message;
        } else if (error?.response?.status === ServerErrorStatus.ValidationErrors) {
            this.serverErrors = error?.response?.data.errors;
        } else {
            this.serverErrorMessage = "Network Error.";
        }
        this.setPopUpIsOpened(true);
    }

    public resetErrors = () => {
        this.serverErrorMessage = "";
        this.serverErrors = {};
    }
}

export const serverErrorHandlingStore = new ServerErrorHandling();
export const ServerErrorHandlingContext = createContext(serverErrorHandlingStore);