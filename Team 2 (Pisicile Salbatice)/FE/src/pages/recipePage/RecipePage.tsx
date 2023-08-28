import { useContext, useEffect } from "react"
import { RecipePageContext } from "./RecipePage.store"
import { useNavigate, useParams } from "react-router-dom";
import { observer } from "mobx-react";
import styles  from "./RecipePage.module.scss";

export const RecipePage = observer(() => {
   const { fetchRecipe, recipeData } = useContext(RecipePageContext);
   const { recipeId } = useParams();
   const navigate = useNavigate();
   
   const ingredients =  recipeData?.ingredients.join(", ");

    useEffect(() => {
        fetchRecipe(Number(recipeId), () => navigate("/"));
    }, [recipeId, fetchRecipe, navigate]);

    return (
        <div className={styles.recipePage}>
          <div className={styles.recipeContainer} key={recipeData?.id}>
            <div className={styles.flexTitle}>
              <div className={styles.recipeTitle}>Title:</div>
              <div className={styles.recipeTitleContent}>{recipeData?.name}</div>
            </div>
            <div className={styles.flexDescription}>
              <div className={styles.recipeDescription}>Description:</div>
              <div className={styles.recipeDescriptionContent}>{recipeData?.description}</div>
            </div>
            <div className={styles.flexIngredients}>
              <div className={styles.recipeIngredients}>Ingredients:</div>
              <div className={styles.recipeIngredientsContent}>
                {ingredients && `${ingredients}.`}
              </div>
            </div>
          </div>
        </div>
    )
});