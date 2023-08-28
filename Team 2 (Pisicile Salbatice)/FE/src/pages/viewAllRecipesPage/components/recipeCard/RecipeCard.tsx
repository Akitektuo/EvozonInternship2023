import { Box, Grid, Paper, Typography } from "@mui/material";
import styles from "./RecipeCard.module.scss";
import { Link } from "react-router-dom";

interface Props {
    name: string;
    description: string;
    path: string;
}

export const RecipeCard = ({ name, description, path }: Props) => (
    <Grid item xs={4} className={styles.recipeContainer}>
        <Link to={path}>
        <Paper elevation={3} className={styles.cardRecipe}>
                <Box paddingX={3} paddingY={3}>
                    <Typography noWrap variant="h5" component="h3">{name}</Typography>
                    <Typography noWrap variant="body1" component="p">{description}</Typography>
                </Box>
            </Paper>
        </Link>
    </Grid>
);