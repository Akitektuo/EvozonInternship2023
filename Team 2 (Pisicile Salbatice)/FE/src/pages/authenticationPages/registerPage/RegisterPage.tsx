import { observer } from "mobx-react";
import { AuthenticationInput } from "../components/authenticationInput/AuthenticationInput";
import { useCallback, useContext, useEffect } from "react";
import { RegisterPageContext } from "./RegisterPage.store";
import { useRegisterValidation } from "./hooks/UseRegisterValidation";
import { useNavigate } from "react-router-dom";
import { RoutesConstants } from "../../../shared/RoutesConstants";
import { AuthenticationContainer } from "../components/authenticationContainer/AuthenticationContainer";
import { AuthenticationButton } from "../components/authenticationButton/AuthenticationButton";

export const RegisterPage = observer(() => {
    const {
        registerData,
        errorData,
        setFirstNameValue,
        setLastNameValue,
        setEmailValue,
        setPasswordValue,
        setConfirmedPasswordValue,
        setFirstNameIsTouched,
        setLastNameIsTouched,
        setEmailIsTouched,
        setPasswordIsTouched,
        setConfirmedPasswordIsTouched,
        createUser,
        reset
    } = useContext(RegisterPageContext);

    const navigate = useNavigate();

    const registerUser = useCallback(async () => {
        const isUserCreated = await createUser();
        if (isUserCreated) {
            navigate(RoutesConstants.LoginRoute);
        }
    }, [createUser, navigate]);

    const sendRegisterData = useRegisterValidation(registerUser, registerData);

    useEffect(() => reset, [reset]);

    return (
        <AuthenticationContainer title="Register">
            <AuthenticationInput
                labelText="First Name"
                type="text"
                value={registerData.firstName}
                onChange={setFirstNameValue}
                onFocus={setFirstNameIsTouched}
                errorMessage={errorData.firstNameError.isTouched ? errorData.firstNameError.message : ""}
                isValid={!errorData.firstNameError.isTouched || !errorData.firstNameError.message} />

            <AuthenticationInput
                labelText="Last Name"
                type="text"
                value={registerData.lastName}
                onChange={setLastNameValue}
                onFocus={setLastNameIsTouched}
                errorMessage={errorData.lastNameError.isTouched ? errorData.lastNameError.message : ""}
                isValid={!errorData.lastNameError.isTouched || !errorData.lastNameError.message} />
            <AuthenticationInput
                labelText="Email"
                type="text"
                value={registerData.email}
                onChange={setEmailValue}
                onFocus={setEmailIsTouched}
                errorMessage={errorData.emailError.isTouched ? errorData.emailError.message : ""}
                isValid={!errorData.emailError.isTouched || !errorData.emailError.message} />
            <AuthenticationInput
                labelText="Password"
                type="password"
                value={registerData.password}
                onChange={setPasswordValue}
                onFocus={setPasswordIsTouched}
                errorMessage={errorData.passwordError.isTouched ? errorData.passwordError.message : ""}
                isValid={!errorData.passwordError.isTouched || !errorData.passwordError.message} />
            <AuthenticationInput
                labelText="Confirm Password"
                type="password"
                value={registerData.confirmedPassword}
                onChange={setConfirmedPasswordValue}
                onFocus={setConfirmedPasswordIsTouched}
                errorMessage={errorData.confirmedPasswordError.isTouched ? errorData.confirmedPasswordError.message : ""}
                isValid={!errorData.confirmedPasswordError.isTouched || !errorData.confirmedPasswordError.message} />
            <AuthenticationButton text="Register" onClick={sendRegisterData} />
        </AuthenticationContainer>
    );
});