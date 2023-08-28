import { Link } from "react-router-dom";
import Fitness from "../../../assets/images/Fitness.jpg";
import FoodLovers from "../../../assets/images/FoodLovers.jpg";
import Gym from "../../../assets/images/Gym.jpg";
import Vegetarian from "../../../assets/images/Vegetarian.jpg";
import { CategoriesEnum } from "../../../shared/CategoriesEnum";
import styles from "./CategoryCard.module.scss";

interface Prop {
    title: string;
    path: string;
}

const CategoryImages: string[] = [Fitness, FoodLovers, Gym, Vegetarian];

export const CategoryCard = ({ title, path }: Prop) => {
    const indexOfCategoryImage = CategoriesEnum[title as keyof typeof CategoriesEnum] - 1;

    return (
        <Link className={styles.categoryCard} to={path}>
            <div className={styles.categoryTitle}>
                {title}
            </div>
            <div className={styles.categoryImage}>
                <img src={CategoryImages[indexOfCategoryImage]} alt={`${title}`} />
            </div>
        </Link>
    )
};