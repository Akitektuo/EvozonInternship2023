import React, { useContext, useEffect } from "react";
import { Box, Button, CircularProgress, Container, Grid } from "@mui/material";
import styles from "./AllRecipes.module.scss";
import { observer } from "mobx-react";
import { allRecipesContext } from "./AllRecipes.store";
import { Link } from "react-router-dom";
import { PaginationComponent } from "./components/Pagination";

export const RecipesPage = observer(() => {
    const { getAllRecipes, isLoading, error, recipes } = useContext(allRecipesContext);
    
    useEffect(() => {
        getAllRecipes();
    }, [getAllRecipes]);

    if (isLoading) {
        return (
            <Box className={styles.statusOverlay}>
                <CircularProgress />
                <p>Loading...</p>
            </Box>
        )
    }

    if (error) {
        return (
            <Container className={styles.statusOverlay}>
                <p>Error: {error}</p>
            </Container>
        )
    }

    return (
        <div className={styles.pageContainer}>
            <Container className={styles.recipesContainer}>
                <Grid container className={styles.gridContainer}>
                    {recipes.map ( item => (
                        <Link to={`/recipe/${item.id}`} key={item.id} className={styles.gridItemContainer}>
                            <h4 className={styles.recipeName}>{item.name}</h4>
                            <p className={styles.recipeDescription}>{item.description}</p>
                        </Link>
                    ))}
                </Grid>
            <Button component={Link} to="createRecipe" className={styles.linkButton}>Create Recipe</Button>
            </Container>
            <PaginationComponent onChange={getAllRecipes} />
        </div>
    )
});