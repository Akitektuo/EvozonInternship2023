import { observer } from "mobx-react";
import { RoutesConstants } from "../shared/RoutesConstants";
import { NavigationBarItem, NavigationBarItemLogout } from "./NavigationBarItem";
import { useUserService } from "../services/userService/UseUserService";
import { RoleEnum } from "../pages/authenticationPages/loginPage/LoginPage.types";
import { useContext, useEffect, useRef } from "react";
import styles from "./NavigationBar.module.scss";
import { NavigationBarContext } from "./Navigationbar.store";

export const NavigationBar = observer(() => {
    const { isAuthenticated, clearUserData, userData } = useUserService(); 
    const navbarHeight = useRef<HTMLDivElement>(null);
    const { setHeightNavigationbar } = useContext(NavigationBarContext);

    useEffect(() => {
        if(navbarHeight.current) {
            const height = navbarHeight.current.clientHeight;
            setHeightNavigationbar(height);
        }
    }, [setHeightNavigationbar]);

    return (
        <div className={styles.navigationBar} ref={navbarHeight}>
            <div className={styles.container}>
                <NavigationBarItem text="Home" url={RoutesConstants.BaseRoute} />
                {userData?.roleId === RoleEnum.ADMIN && 
                    <>
                        <NavigationBarItem text="Add Recipe" url={RoutesConstants.AddRecipeRoute} />
                        <NavigationBarItem text="Recipes" url={`${RoutesConstants.RecipesRoute}?page=1`} />
                        <NavigationBarItem text="Generate Menu" url={RoutesConstants.GenerateMenusRoute} />
                    </>
                }
            </div>
            {isAuthenticated() ? (
                <div className={styles.container}>
                    <p className={styles.greeting}>Hello, {userData?.firstName}</p>
                    <NavigationBarItemLogout text="Logout" url={RoutesConstants.BaseRoute} onClick={clearUserData} />
                </div> 
            ) : (
                <div className={styles.container}>
                    <NavigationBarItem text="Register" url={RoutesConstants.RegisterRoute} />
                    <NavigationBarItem text="Login" url={RoutesConstants.LoginRoute} />
                </div> 
            )}
        </div>
    );
});