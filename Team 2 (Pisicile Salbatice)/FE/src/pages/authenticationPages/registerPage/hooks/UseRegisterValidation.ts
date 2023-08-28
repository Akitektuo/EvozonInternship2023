import { useContext } from "react";
import { RegisterPageContext } from "../RegisterPage.store";
import { emailValidation, nameValidation, passwordValidation, confirmedPasswordValidation } from "../../../../shared/ValidationInputs";
import { RegisterUserType } from "../RegisterPage.types";

export const useRegisterValidation = (onSuccess: () => Promise<void>, registerData: RegisterUserType) => {
    const { 
        errorData,
        setFirstNameError,
        setLastNameError,
        setEmailError,
        setPasswordError,
        setConfirmedPasswordError
    } = useContext(RegisterPageContext);

    setFirstNameError(nameValidation(registerData.firstName));
    setLastNameError(nameValidation(registerData.lastName));
    setEmailError(emailValidation(registerData.email));
    setPasswordError(passwordValidation(registerData.password));
    setConfirmedPasswordError(confirmedPasswordValidation(registerData.password, registerData.confirmedPassword));

    const sendRegisterData = async () => {
        if (errorData.firstNameError.message === "" && 
            errorData.lastNameError.message === "" && 
            errorData.emailError.message === "" && 
            errorData.passwordError.message === "" && 
            errorData.confirmedPasswordError.message === "") {
            await onSuccess();
        }
    }

    return sendRegisterData;
}