import { ErrorDetails } from "../loginPage/LoginPage.types";

export interface RegisterUserType {
    firstName: string;
    lastName: string;
    email: string;
    password: string;
    confirmedPassword: string;
}

export const EMPTY_REGISTER_USER: RegisterUserType = {
    firstName: "",
    lastName: "",
    email: "",
    password: "",
    confirmedPassword: ""
};

export interface RegisterErrorType {
    firstNameError: ErrorDetails;
    lastNameError: ErrorDetails;
    emailError: ErrorDetails;
    passwordError: ErrorDetails;
    confirmedPasswordError: ErrorDetails;
}

export const EMPTY_REGISTER_ERROR: RegisterErrorType = {
    firstNameError: {
        message: "",
        isTouched: false
    },
    lastNameError: {
        message: "",
        isTouched: false
    },
    emailError: {
        message: "",
        isTouched: false
    },
    passwordError: {
        message: "",
        isTouched: false
    },
    confirmedPasswordError: {
        message: "",
        isTouched: false
    }
};