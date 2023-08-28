import React, { useContext, useEffect } from "react";
import styles from "./RegisterPage.module.scss";
import { observer } from "mobx-react";
import { Box, Container, Button } from "@mui/material";
import { Link, useNavigate } from "react-router-dom";
import { InputComponent } from "../components/inputComponent/InputComponent";
import { EMAIL_PATTERN, PASSWORD_PATTERN, FIRST_NAME_PATTERN, LAST_NAME_PATTERN } from "../../shared/Patterns";
import { registerPageContext } from "./RegisterPage.store";
import { useRegisterValidation } from "./UseRegisterLogic";

export const RegisterPage = observer(() => {
    const { 
        userInformation, 
        setFirstName, 
        setLastName, 
        setEmail, 
        setPassword, 
        setConfirmPassword,
        registerUser, 
        firstNameError, 
        lastNameError, 
        emailError, 
        passwordError, 
        confirmPasswordError,
        resetRegisterPage
    } = useContext(registerPageContext);

    useEffect(() => resetRegisterPage, [resetRegisterPage]);

    useRegisterValidation();

    const navigate = useNavigate();

    const handleRegister = async () => {   
        const success = await registerUser(userInformation);
        if (success) {
            navigate("/login-page");
        }
    }
    const handleSubmit = (event: any) => {
        event.preventDefault();
        handleRegister();
    }

    return (
        <Container className={styles.formContainer}>
            <Box className={styles.boxRoot}>
                <h2>Create Your Account</h2>
                <p><span>Already having an account?</span> <Link to="/login-page">Sign In Here</Link></p>
            </Box>
            <form className={styles.formControl} onSubmit={handleSubmit}>
                <InputComponent
                    pattern={FIRST_NAME_PATTERN}
                    onChange={setFirstName}
                    placeHolder="First Name"
                    value={userInformation.firstName}
                    type="text"
                    errorMessage={firstNameError} />
                <InputComponent
                    pattern={LAST_NAME_PATTERN}
                    onChange={setLastName}
                    placeHolder="Last Name"
                    value={userInformation.lastName}
                    type="text"
                    errorMessage={lastNameError} />
                <InputComponent
                    pattern={EMAIL_PATTERN}
                    onChange={setEmail}
                    placeHolder="Email"
                    value={userInformation.email}
                    type="email"
                    errorMessage={emailError} />
                <InputComponent
                    pattern={PASSWORD_PATTERN}
                    onChange={setPassword}
                    placeHolder="Password"
                    value={userInformation.password}
                    type="password"
                    errorMessage={passwordError} />
                <InputComponent
                    pattern={PASSWORD_PATTERN}
                    onChange={setConfirmPassword}
                    placeHolder="Confirm Password"
                    value={userInformation.confirmPassword}
                    type="password"
                    errorMessage={confirmPasswordError} />
                <Button type="submit" onClick={handleSubmit} className={styles.submitButton}>Create Account</Button>
            </form>
        </Container>
    );
});