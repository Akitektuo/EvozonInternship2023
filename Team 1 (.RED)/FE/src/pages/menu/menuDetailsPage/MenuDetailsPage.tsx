import React, { useEffect } from "react";
import { useContext } from "react";
import { menuDetailsContext } from "./MenuDetailsPage.store";
import { useParams } from "react-router-dom";
import { Box, CircularProgress } from "@mui/material";
import { observer } from "mobx-react";
import { MenuDetails } from "../components/MenuDetails";
import styles from "./MenuDetailsPage.module.scss";

export const MenuDetailsPage = observer(() => {
    const { menu, isLoading, getMenuDetailsById } = useContext(menuDetailsContext);
    const { id } = useParams();

    useEffect(() => {
        if (id) {
            getMenuDetailsById(id);
        }
    }, [id, getMenuDetailsById]);

    if (isLoading) {
        return (
            <Box className={styles.container}>
                <CircularProgress />
                <p>Loading ... </p>
            </Box>
        );
    }

    return (
        <MenuDetails menu={menu}/>
    );
});