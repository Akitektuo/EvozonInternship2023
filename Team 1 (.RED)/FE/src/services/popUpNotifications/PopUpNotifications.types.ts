export enum MessageType {
    Error,
    Success,
    Info
}

export interface PopUpMessage {
    title: string;
    message: string;
    errorObject: {[key: string]: string[]};
    type: MessageType;
}