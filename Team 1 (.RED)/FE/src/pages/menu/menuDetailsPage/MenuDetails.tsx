import React, { useEffect } from "react";
import { useContext } from "react";
import { useParams } from "react-router-dom";
import { Box, CircularProgress } from "@mui/material";
import { observer } from "mobx-react";
import styles from "./MenuDetails.module.scss";
import { MoreHorizTwoTone, NotesTwoTone } from "@mui/icons-material";
import { MealType } from "../../../shared/Types";
import { menuDetailsContext } from "./MenuDetailsPage.store";

const getMealTypeById = (id: number) => {
    const mealIndex = Object.values(MealType).indexOf(id as unknown as MealType);
    return Object.keys(MealType)[mealIndex];
}

export const MenuDetails = observer(() => {
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
        <div className={styles.menuContainer}>
            <h1>Menu Details</h1>
            <div className={styles.menuCard}>
                <div>
                    <h3><NotesTwoTone className={styles.icon} />Name: </h3>
                    {menu?.name}
                </div>
                <div>
                    <h3><MoreHorizTwoTone className={styles.icon}/> Description:</h3>
                    <p>{menu?.description}</p>
                </div>
                <div>
                    <h3>Price</h3>
                    <p>&euro; {menu?.price}</p>
                </div>
                <div>
                    {menu?.mealsDetails.map((meal, index) => (
                        <div key={index}>
                            <h4>{getMealTypeById(meal.mealTypeId)}</h4>
                            <p>{meal.name}</p>
                            <div>{meal.recipeDetails.description}</div>
                            <div className={styles.ingredientsContainer}>
                                {meal.recipeDetails.ingredients.map((ingredient, index) => (
                                    <p className={styles.ingredientName} key={index}>{ingredient}</p>
                                ))}
                            </div>
                        </div>
                    ))}
                </div>
            </div>
        </div>
    );
});