import { Box, Grid, Paper, Typography } from "@mui/material";
import { MealEnum, MealType } from "../MenuPage.types";
import styles from "./MenuDetails.module.scss";

interface Props {
    meals: MealType[];
}

export const MealsList = ({ meals }: Props) => (
    <Grid container spacing={5} >
        {meals.map((meal) => 
        <Grid item xs={6} key={meal.id}>
            <Paper elevation={3} className={styles.menuCard}>
                <Box marginTop={2} padding={2} className={styles.menuItemContainer}>
                    <Typography variant="h5" component="h4">
                        {meal.name}
                    </Typography>
                    <Typography variant="subtitle1" component="p">
                        Category: {MealEnum[meal.mealTypeId]}
                    </Typography>
                    <Typography>Description: {meal.description}</Typography>
                    <Typography>Ingredients: {meal.ingredients.join(", ")}</Typography>
                    <Typography variant="subtitle2" component="p" className={styles.priceItem}>
                        Price: {Number(meal.price.toFixed(2))} $
                    </Typography>
                </Box>
            </Paper>
        </Grid>)}
    </Grid>
);