import React, { useContext, useEffect } from "react";
import { observer } from "mobx-react";
import SearchIcon from '@mui/icons-material/Search';
import { Box } from "@mui/material";
import { addRecipePageContext } from "../AddRecipePage.store";
import styles from "./SearchBarInput.module.scss"

export const SearchBarInput = observer(() => {
    const { handleInputValue, inputValue } = useContext(addRecipePageContext);

    return (
        <Box className={styles.inputContainer}>
            <SearchIcon />
            <input type="text" value={inputValue} placeholder="Search Ingredients..." onChange={event => handleInputValue(event.target.value)}/>
        </Box>
    )
})