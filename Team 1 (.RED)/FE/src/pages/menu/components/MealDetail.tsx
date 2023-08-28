import { MealDetails, MealType } from "../../../shared/Types";
import styles from "./MealDetail.module.scss";

interface Props {
    meal: MealDetails,
    index: number
}

const getMealTypeById = (id: number) => {
    const mealIndex = Object.values(MealType).indexOf(id as unknown as MealType);
    return Object.keys(MealType)[mealIndex];
}

export const MealDetail = ({ meal, index }: Props) => (
    <>
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
    </>
)