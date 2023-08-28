import { UserServiceContext } from "./UserService.store";
import { useContext, useEffect } from "react";

export const useUserService = (initializer: boolean = false) => {
    const { userData, isAuthenticated, clearUserData, initialize, isUserInitialized } = useContext(UserServiceContext);

    useEffect(() => {
        if (initializer) {
            initialize();
        }
    }, [initialize, initializer]);

    return { userData, isAuthenticated, clearUserData, isUserInitialized };
}