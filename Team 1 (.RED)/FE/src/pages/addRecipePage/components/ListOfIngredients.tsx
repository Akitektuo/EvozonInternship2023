import React from "react";
import { useContext } from "react";
import { addRecipePageContext } from "../AddRecipePage.store";
import styles from "./ListOfIngredients.module.scss";
import { observer } from "mobx-react";

export const ListOfIngredients = observer(() => {
    const { filteredListOfIngredients, inputValue, addIngredient } = useContext(addRecipePageContext);

    return (
        <>
            {inputValue && <ul className={styles.listOfIngredients}>
                {filteredListOfIngredients.map(ingredient => {
                    const handleClick = () => {
                        addIngredient(ingredient);
                    }
                    return (
                        <div onClick={handleClick} className={styles.ingredientContainer} key={ingredient.id}>{ingredient.name}</div>
                    )
                })}
            </ul>}
        </>
    )
})