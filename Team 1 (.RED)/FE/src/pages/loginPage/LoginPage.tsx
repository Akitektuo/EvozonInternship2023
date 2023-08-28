import React, { useContext, useEffect } from "react";
import { Link, useNavigate } from "react-router-dom";
import "./LoginPage.module.scss";
import { Button, Box, Container } from "@mui/material";
import styles from "./LoginPage.module.scss";
import { observer } from "mobx-react";
import { loginPageContext } from "./LoginPage.store";
import { InputComponent } from "../components/inputComponent/InputComponent";

export const LoginPage = observer(() => {
    const {
        userCredentials,
        userAuthentication,
        setEmail,
        setPassword,
        resetCredentials 
    } = useContext(loginPageContext);

    const navigate = useNavigate();

    useEffect(() => resetCredentials, [resetCredentials]);

    const handleLogin = async () => {
        const isAuthenticated = await userAuthentication(userCredentials);
        if (isAuthenticated) {
            navigate("/");
        }
    }
    const handleSubmit = (event: any) => {
        event.preventDefault();
        handleLogin();
    }
 
    return ( 
        <Container className={styles.formContainer}>
            <Box className={styles.boxRoot}>
                <h2>Log In Your Account</h2>
                <p><span>Don't have an account yet?</span> <Link to="/register-page">Create One Here</Link></p>
            </Box>
            <form className={styles.formControl} onSubmit={handleSubmit}>
                <InputComponent 
                    onChange={setEmail}
                    placeHolder="Email"
                    value={userCredentials.email} 
                    required />
                <InputComponent 
                    onChange={setPassword}
                    placeHolder="Password"
                    value={userCredentials.password}
                    type="password" 
                    required />
                <Button type="submit" onClick={handleSubmit} className={styles.submitButton}>Submit</Button>
            </form>
        </Container>
    );
});