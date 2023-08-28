export interface LoginType {
    email: string;
    password: string;
}

export const EMPTY_LOGIN: LoginType = {
    email: "",
    password: ""
};

export interface ErrorDetails {
    message: string;
    isTouched: boolean;
}

export interface LoginErrorType {
    emailError: ErrorDetails;
    passwordError: ErrorDetails;
}

export const EMPTY_LOGIN_ERROR: LoginErrorType = {
    emailError: {
        message: "",
        isTouched: false
    },
    passwordError: {
        message: "",
        isTouched: false
    }
};

export interface UserType {
    id?: number;
    firstName: string;
    lastName: string;
    email: string;
    roleId: RoleEnum;
    token: string;
}

export enum RoleEnum {
    USER = 1,
    ADMIN = 2
}