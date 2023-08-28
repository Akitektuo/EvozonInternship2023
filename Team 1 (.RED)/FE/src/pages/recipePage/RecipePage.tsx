import { useParams } from "react-router-dom";
import styles from "./RecipePage.module.scss";
import { useContext, useEffect } from "react";
import { recipePageContext } from "./RecipePage.store";
import { observer } from "mobx-react";
import { Box, CircularProgress } from "@mui/material";
import { NotesTwoTone } from "@mui/icons-material";
import { MoreHorizTwoTone } from "@mui/icons-material";
import { DiningTwoTone } from "@mui/icons-material";

export const RecipePage = observer(() => {
    const { recipe, getRecipeById, isLoading: loading } = useContext(recipePageContext);
    const { id } = useParams();

    useEffect(() => {
        if (id) {
            getRecipeById(id);
        }
    }, [id, getRecipeById]);

    if (loading) {
        return (
            <Box className={styles.recipeContainer}>
                <CircularProgress />
                <p>Loading ... </p>
            </Box>
        );
    }

    return (
        <div className={styles.recipeContainer}>
            <h1>Recipe page</h1>
            <div className={styles.recipeCard}>
                <div>
                    <h3><NotesTwoTone className={styles.icon} />Name: </h3>
                    {recipe?.name}
                </div>
                <div>
                    <h3><MoreHorizTwoTone className={styles.icon}/> Description:</h3>
                    <p>{recipe?.description}</p>
                </div>
                <div>
                    <h3><DiningTwoTone className={styles.icon}/> Ingredients:</h3>
                    <ul>
                        {recipe?.ingredients.map((ingredient, index) => (
                            <li key={ingredient}>{ingredient}</li>
                        ))}
                    </ul>
                </div>
            </div>
        </div>
    );
});