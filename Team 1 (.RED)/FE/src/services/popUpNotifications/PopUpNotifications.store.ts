import { makeAutoObservable } from "mobx";
import { createContext } from "react";
import { MessageType, PopUpMessage } from "./PopUpNotifications.types";

export class PopUpNotificationStore {
    public messageInfo: PopUpMessage | null = null;

    constructor() {
        makeAutoObservable(this);
    }

    public setMessageInfo = (messageInfo: PopUpMessage) => {
        this.messageInfo = messageInfo;
    }

    public resetMessage = () => {
        this.messageInfo = null;
    }

    public handleError = (error: unknown | any) => {
        const popUpError = error.response.data;
        console.log(popUpError);
        const popUpMessage: PopUpMessage = {
            title: popUpError.title ?? "OOPS...",
            message: popUpError.message ?? "Something went wrong:",
            errorObject: popUpError.errors,
            type: MessageType.Error
        }
        this.messageInfo = popUpMessage;

    }

    public showSuccess = (title: string, message: string) => this.showNotification(title, message, MessageType.Success);

    public showError = (title: string, message: string) => this.showNotification(title, message, MessageType.Error);

    private showNotification = (title: string, message: string, type: MessageType) => {
        this.messageInfo = {
            title,
            message,
            errorObject: {},
            type
        };
    }
}

export const popUpNotificationStore = new PopUpNotificationStore();
export const popUpNotificationContext = createContext(popUpNotificationStore);