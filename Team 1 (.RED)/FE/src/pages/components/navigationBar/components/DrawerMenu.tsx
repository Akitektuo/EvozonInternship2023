import React, { useContext } from "react";
import { Box, Typography, Divider, List, ListItem, ListItemButton, ListItemText } from "@mui/material";
import { Link } from "react-router-dom";
import styles from "./DrawerMenu.module.scss";
import { userDetailsContext } from "../../../../services/UserDetails.store";

interface Props {
    onToggle: () => void;
}

export const DrawerMenu = ({ onToggle }: Props) => {
    const { isLoggedIn, clearUser, userDetails } = useContext(userDetailsContext);

    return (
        <Box onClick={onToggle} className={styles.drawerContainer}>
            <Typography className={styles.applicationName} component={Link} to="/" variant="h6">
                MealPlanApp
            </Typography>
            <Divider className={styles.divider} />
            <List className={styles.drawerList}>
                {userDetails?.role === "Admin" && 
                    <ListItem className={styles.listItem} disablePadding>
                        <ListItemButton component={Link} className={styles.listItemButton} to="recipes">
                            <ListItemText>Recipes</ListItemText>
                        </ListItemButton>
                    </ListItem>
                }
                <ListItem className={styles.listItem} disablePadding>
                    {isLoggedIn() ? (
                        <ListItemButton className={styles.listItemButton} onClick={clearUser} component={Link} to="/login-page">
                            <ListItemText primary="Logout" />
                        </ListItemButton>
                    ) : (
                        <ListItemButton className={styles.listItemButton} component={Link} to="/login-page">
                            <ListItemText primary="Login" />
                        </ListItemButton>
                    )} 
                </ListItem>
            </List>
        </Box>
    );
};