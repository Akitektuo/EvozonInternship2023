import { useContext, useEffect } from "react";
import { EMAIL_PATTERN, FIRST_NAME_PATTERN, LAST_NAME_PATTERN, PASSWORD_PATTERN } from "../../shared/Patterns";
import { registerPageContext } from "./RegisterPage.store";

const checkFirstNameValidations = (firstNameString: string) => {
    const firstNameRegex = new RegExp(FIRST_NAME_PATTERN);
    if (firstNameString.length < 3 || firstNameString.length > 50) {
        return "First Name must be between 3 and 50 characters";
    }
    if (!firstNameRegex.test(firstNameString)) {
        return "First Name must start with capital letter and can't have special characters";
    }
    return "";
}

const checkLastNameValidations = (lastNameString: string) => {
    const lastNameRegex = new RegExp(LAST_NAME_PATTERN);
    if (lastNameString.length < 3 || lastNameString.length > 50) {
        return "Last Name must be between 3 and 50 characters";
    }
    if (!lastNameRegex.test(lastNameString)) {
        return "Last Name must start with capital letter and can't have special characters";
    }
    return "";
}

const checkEmailValidations = (emailString: string) => {
    const emailRegex = new RegExp(EMAIL_PATTERN);
    if (emailString.length < 7 || emailString.length > 50) {
        return "Email must be between 7 and 50 characters";
    }
    if (!emailRegex.test(emailString)) {
        return "Invalid email format";
    }
    return "";
}

const checkPasswordValidations = (passwordString: string) => {
    const passwordRegex = new RegExp(PASSWORD_PATTERN);
    if (passwordString.length < 5 || passwordString.length > 20) {
        return "Password length must be between 5 and 20 characters";
    }
    if (!passwordRegex.test(passwordString)) {
        return "Password cannot end with space";
    }
    return "";
}

const checkConfirmPasswordValidations = (passwordString: string, confirmPasswordString: string) => {
    if (confirmPasswordString !== passwordString) {
        return "Passwords must match";
    }
    return "";
}

export const useRegisterValidation = () => {
    const { 
        userInformation,
        setEmailError,
        setPasswordError,
        setFirstNameError,
        setLastNameError,
        setConfirmPasswordError,
        isFirstNameTouched,
        isLastNameTouched,
        isEmailTouched,
        isPasswordTouched,
        isConfirmPasswordTouched 
    } = useContext(registerPageContext);

    useEffect(() => {
        if (isFirstNameTouched) {
            setFirstNameError(checkFirstNameValidations(userInformation.firstName));
        }
    }, [setFirstNameError, userInformation.firstName, isFirstNameTouched]);

    useEffect(() => {
        if (isLastNameTouched) {
            setLastNameError(checkLastNameValidations(userInformation.lastName));
        }
    }, [setLastNameError, userInformation.lastName, isLastNameTouched]);

    useEffect(() => {
        if (isEmailTouched) {
            setEmailError(checkEmailValidations(userInformation.email));
        }
    }, [setEmailError, userInformation.email, isEmailTouched]);

    useEffect(() => {
        if (isPasswordTouched) {
            setPasswordError(checkPasswordValidations(userInformation.password));
        }
    }, [setPasswordError, userInformation.password, isPasswordTouched]);

    useEffect(() => {
        if (isConfirmPasswordTouched) {
            setConfirmPasswordError(checkConfirmPasswordValidations(userInformation.password, userInformation.confirmPassword));
        }
    }, [setConfirmPasswordError, userInformation.confirmPassword, userInformation.password, isConfirmPasswordTouched]);
}