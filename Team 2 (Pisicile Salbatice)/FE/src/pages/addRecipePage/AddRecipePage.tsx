import {
    Box,
    Button,
    Container,
    FormControl,
    Grid,
    InputLabel,
    Paper,
    TextField,
    Typography
} from "@mui/material";
import styles from "./AddRecipePage.module.scss";
import { observer } from "mobx-react";
import { useContext, useEffect } from "react";
import { AddRecipePageContext } from "./AddRecipePage.store";
import { useNavigate } from "react-router-dom";
import { RoutesConstants } from "../../shared/RoutesConstants";
import { IngredientsSelect } from "./components/ingredientsSelect/IngredientsSelect";
import { ChipItemsList } from "./components/chipItemsList/ChipItemsList";
import { ERROR_MESSAGE } from "./AddRecipePage.types";

export const AddRecipePage = observer(() => {
    const {
        recipe,
        ingredientsList,
        selectedIngredients,
        recipeIsTouched,
        setRecipeName,
        setRecipeDescription,
        setNameIsTouched,
        setDescriptionIsTouched,
        setIngredientIdsIsTouched,
        addIngredient,
        removeIngredient,
        reset,
        createRecipe,
        fetchIngredients
    } = useContext(AddRecipePageContext);
 
    const isDisabled = !recipe.name || !recipe.description || !selectedIngredients.length;

    const navigate = useNavigate();

    const handleSubmit = async () => {
        if (!recipe.name ||
            !recipe.description ||
            !selectedIngredients.length) {
            return;
        }
        const isRecipeCreated = await createRecipe();
        if (isRecipeCreated) {
            reset();
            navigate(`${RoutesConstants.RecipesRoute}?page=1`);
        }
    }

    useEffect(() => {
        fetchIngredients();
        return reset;
    }, [fetchIngredients, reset]);

    return (
        <div className={styles.addRecipePage}>
            <Container maxWidth="sm" className={styles.addRecipeContainer}>
                <Typography align="center" variant="h2" component="h1">Add recipe</Typography>
                <TextField
                    label="Title"
                    variant="filled"
                    fullWidth
                    margin="normal"
                    value={recipe.name}
                    onChange={(event) => setRecipeName(event.target.value)}
                error={recipeIsTouched.nameIsTouched && !recipe.name}
                onFocus={setNameIsTouched}
                    />
            <p className={styles.errorMessage}>
                {(recipeIsTouched.nameIsTouched && !recipe.name) && ERROR_MESSAGE}
            </p>
                <TextField
                    label="Description"
                    multiline
                    rows={4}
                    variant="filled"
                    fullWidth
                    margin="normal"
                    value={recipe.description}
                    onChange={(event) => setRecipeDescription(event.target.value)}
                error={recipeIsTouched.descriptionIsTouched && !recipe.description}
                onFocus={setDescriptionIsTouched}
                    />
            <p className={styles.errorMessage}>
                {(recipeIsTouched.descriptionIsTouched && !recipe.description) && ERROR_MESSAGE}
            </p>
                <Grid container spacing={2}>
                <Grid item xs={6} marginY={2}>
                        <Box>
                        <FormControl 
                            fullWidth 
                            variant="filled" 
                            error={recipeIsTouched.ingredientIdsIsTouched && !selectedIngredients.length} 
                            onFocus={setIngredientIdsIsTouched}>
                            <InputLabel shrink={false}>Ingredients</InputLabel>
                                <IngredientsSelect ingredientsList={ingredientsList} label="Ingredients" onChange={addIngredient} />
                            </FormControl>
                        <p className={styles.errorMessage}>
                            {(recipeIsTouched.ingredientIdsIsTouched && !selectedIngredients.length) && ERROR_MESSAGE}
                        </p>
                        </Box>
                    </Grid>
                <Grid item xs={6} marginY={2}>
                        <Paper className={styles.chipContainer}>
                            <ChipItemsList selectedIngredients={selectedIngredients} onDelete={removeIngredient} />
                        </Paper>
                    </Grid>
                </Grid>
            <Button variant="contained" onClick={handleSubmit} disabled={isDisabled}>Submit</Button>
            </Container>
        </div>
    );
});