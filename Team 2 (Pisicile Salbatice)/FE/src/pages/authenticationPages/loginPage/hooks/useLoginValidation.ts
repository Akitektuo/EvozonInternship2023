import { useContext } from "react";
import { LoginPageContext } from "../LoginPage.store";
import { emailValidation, passwordValidation } from "../../../../shared/ValidationInputs";

export const useLoginValidation = (fetchUser: () => Promise<void>) => { 
    const { 
        loginData: { email, password }, 
        errorData,
        setEmailError, 
        setPasswordError,
    } = useContext(LoginPageContext);

    setEmailError(emailValidation(email));
    setPasswordError(passwordValidation(password));

    const validateError = async () => {
        if (errorData.emailError.message === "" && errorData.passwordError.message === "") {
            await fetchUser();
        }
    }

    return validateError;
};