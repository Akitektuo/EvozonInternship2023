import { useContext, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { observer } from "mobx-react";
import { LoginPageContext } from "./LoginPage.store";
import { RoutesConstants } from "../../../shared/RoutesConstants";
import { useUserService } from "../../../services/userService/UseUserService";
import { AuthenticationInput } from "../components/authenticationInput/AuthenticationInput";
import { AuthenticationButton } from "../components/authenticationButton/AuthenticationButton";
import { AuthenticationContainer } from "../components/authenticationContainer/AuthenticationContainer";
import { useLoginValidation } from "./hooks/useLoginValidation";

export const LoginPage = observer(() => {
    const {
        loginData,
        errorData,
        setEmailValue,
        setPasswordValue,
        fetchUser,
        setEmailIsTouched,
        setPasswordIsTouched,
        reset
    } = useContext(LoginPageContext);

    const { isAuthenticated } = useUserService();

    const validateError = useLoginValidation(fetchUser);

    const navigate = useNavigate();

    const handleClick = async () => {
        await validateError();
        if (isAuthenticated()) {
            navigate(RoutesConstants.BaseRoute);
        }
    }

    useEffect(() => reset, [reset]);

    return (
        <AuthenticationContainer title="Login">
            <AuthenticationInput
                labelText="Email:"
                type="text"
                value={loginData.email}
                onChange={setEmailValue}
                onFocus={setEmailIsTouched}
                errorMessage={errorData.emailError.isTouched ? errorData.emailError.message : ""}
                isValid={!errorData.emailError.isTouched || !errorData.emailError.message} />
            <AuthenticationInput
                labelText="Password:"
                type="password"
                value={loginData.password}
                onChange={setPasswordValue}
                onFocus={setPasswordIsTouched}
                errorMessage={errorData.passwordError.isTouched ? errorData.passwordError.message : ""}
                isValid={!errorData.passwordError.isTouched || !errorData.passwordError.message} />
            <AuthenticationButton text="Login" onClick={handleClick} />
            <AuthenticationButton text="Register" onClick={() => navigate(RoutesConstants.RegisterRoute)} />
        </AuthenticationContainer>
    );
});