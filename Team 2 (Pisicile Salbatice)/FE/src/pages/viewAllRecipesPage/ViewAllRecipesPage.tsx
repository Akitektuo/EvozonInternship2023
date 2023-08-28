import { observer } from "mobx-react";
import { useCallback, useContext, useEffect } from "react";
import { ViewAllRecipesPageContext } from "./ViewAllRecipesPage.store";
import { Container, Grid, Pagination, Stack } from "@mui/material";
import { RecipeCard } from "./components/recipeCard/RecipeCard";
import { useNavigate, useSearchParams } from "react-router-dom";
import { RoutesConstants } from "../../shared/RoutesConstants";
import { calculatePageCount } from "../../shared/CalculatePageCount";
import styles from "./ViewAllRecipesPage.module.scss";

export const ViewAllRecipesPage = observer(() => {
    const {
        paginationData,
        recipeList,
        getAllRecipes
    } = useContext(ViewAllRecipesPageContext);

    const navigate = useNavigate();

    const [searchParams, setSearchParams] = useSearchParams();
    
    useEffect(() => {
        getAllRecipes(() => navigate(RoutesConstants.BaseRoute), Number(searchParams.get("page")));
    }, [getAllRecipes, navigate, searchParams]);

    const handleChange = useCallback((page: number) => {
        getAllRecipes(() => navigate(RoutesConstants.BaseRoute), Number(searchParams.get("page")));
        setSearchParams({"page": page.toString()});
        navigate(`${RoutesConstants.RecipesRoute}?page=${page}`);
    }, [setSearchParams, getAllRecipes, navigate]);

    const recipeListComponent = recipeList?.recipesList.map((recipe) => {
        return (
            <RecipeCard
                name={recipe.name}
                description={recipe.description}
                key={recipe.id}
                path={`${RoutesConstants.RecipesRoute}/${recipe.id}`} />
        );
    });

    const pageCount = calculatePageCount(recipeList?.totalRecipesCount, paginationData);

    return (
        <div className={styles.viewAllRecipePage}>
            <Container className={styles.allRecipesContainer}>
                <Grid container spacing={5}>
                    {recipeListComponent}
                </Grid>
            </Container>
            <Stack alignItems="center" className={styles.paginationContainer}>
                <Pagination count={pageCount} page={Number(searchParams.get("page"))} onChange={(_, page) => handleChange(page)} />
            </Stack>
        </div>
    );
});