export interface ServerErrosTypes {
    [field: string]: string[];
}

export enum ServerErrorStatus {
    ValidationErrors = 400,
    BadRequest = 409
}