import React, { useEffect } from "react";
import { SearchBarInput } from "./components/SearchBarInput";
import { observer } from "mobx-react";
import { useContext } from "react";
import { addRecipePageContext } from "./AddRecipePage.store";
import { Box, Button } from "@mui/material";
import styles from "./AddRecipePage.module.scss";
import { ListOfIngredients } from "./components/ListOfIngredients";
import ClearIcon from '@mui/icons-material/Clear';
import classNames from "classnames";
import { useAddRecipeLogic } from "./UseAddRecipeLogic";

export const AddRecipePage = observer(() => {
    const { 
        selectedIngredients, 
        removeIngredient, 
        setRecipeName, 
        setRecipeDescription, 
        addRecipe, 
        recipeName, 
        recipeDescription, 
        getIngredients 
    } = useContext(addRecipePageContext);
    const isFormDisabled = useAddRecipeLogic();

    useEffect(() => {
        getIngredients();
    }, [getIngredients]);

    return (
        <Box className={styles.container}>
            <Box className={styles.searchContainer}>
                <SearchBarInput />
                <ListOfIngredients />
            </Box>
            <form className={styles.createRecipeContainer}>
                <input className={styles.recipeNameInput} value={recipeName} type="text" placeholder="Recipe's name" onChange={event => setRecipeName(event.target.value)}/>
                <h4>List Of Ingredients:</h4>
                <Box className={styles.selectedIngredientsList}>
                    {selectedIngredients && selectedIngredients.map((ingredient, index) => {
                        const handleRemoveIngredient = () => {
                            removeIngredient(ingredient);
                        }
                        return (
                            <div 
                                className={styles.selectedIngredient} 
                                key={index}
                                onClick={handleRemoveIngredient}>
                                    <ClearIcon className={classNames(styles.clearButton, styles.child)}/>
                                    <span className={styles.child}>{ingredient.name}</span>
                            </div>
                        );
                    })}
                </Box>
                <textarea placeholder="Describe your recipe..." value={recipeDescription} className={styles.recipeDescription} onChange={event => setRecipeDescription(event.target.value)}/>
                {isFormDisabled() && <div className={styles.errorMessage}>* All fields are required</div>}
                <Button className={styles.submitButton} onClick={addRecipe} disabled={isFormDisabled()}>Create Recipe</Button>
            </form>
        </Box>
    );
});