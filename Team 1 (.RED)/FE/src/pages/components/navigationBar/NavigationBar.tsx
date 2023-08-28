import { useContext } from "react";
import { Outlet, Link } from "react-router-dom";
import {
    AppBar,
    Box,
    CssBaseline,
    Drawer,
    IconButton,
    Toolbar,
    Typography,
    Button
} from "@mui/material";
import MenuIcon from "@mui/icons-material/Menu";
import styles from "./NavigationBar.module.scss";
import { DrawerMenu } from "./components/DrawerMenu";
import { observer } from "mobx-react";
import { NavigationBarContext } from "./NavigationBar.store";
import { userDetailsContext } from "../../../services/UserDetails.store";

export const NavigationBar = observer(() => {
    const { isDrawerVisible, handleDrawerToggle } = useContext(NavigationBarContext);
    const { isLoggedIn, clearUser, userDetails } = useContext(userDetailsContext);

    return (
        <Box className={styles.flexContainer}>
            <CssBaseline />
            <AppBar className={styles.applicationBar} component="nav">
                <Toolbar className={styles.toolbar}>
                    <IconButton className={styles.iconButton}
                        color="inherit"
                        aria-label="open drawer"
                        edge="start"
                        onClick={handleDrawerToggle}>
                        <MenuIcon />
                    </IconButton>
                    <Typography className={styles.applicationName}
                        variant="h6"
                        component={Link} to="/">
                        MealPlanApp
                    </Typography>
                    <Box className={styles.buttonBox}>
                        {userDetails?.role === "Admin" &&
                            <Button component={Link} className={styles.itemButton} to="recipes">Recipes</Button>
                        }
                        {isLoggedIn() ? (
                            <Button className={styles.itemButton} onClick={clearUser}>
                                Logout
                            </Button>
                        ) : (
                            <Button className={styles.itemButton} component={Link} to="/login-page">
                                Login
                            </Button>
                        )}
                    </Box>
                </Toolbar>
            </AppBar>
            <Box component="nav">
                <Drawer className={styles.drawer}
                    variant="temporary"
                    open={isDrawerVisible}
                    onClose={handleDrawerToggle}
                    ModalProps={{
                        keepMounted: true,
                    }}>
                    <DrawerMenu onToggle={handleDrawerToggle} />
                </Drawer>
            </Box>
            <div className={styles.mainContent}>
                <Outlet />
            </div>
        </Box>
    );
});