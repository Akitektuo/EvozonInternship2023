import React from "react";
import { Link } from "react-router-dom";
import { MenuCategory } from "../../../shared/Types";
import styles from "./MenuCategories.module.scss";
import Fitness from "../../../shared/menuCategoriesImages/fitness.jpg";
import FoodLover from "../../../shared/menuCategoriesImages/foodLover.jpg";
import Gym from "../../../shared/menuCategoriesImages/gym.jpg";
import Vegetarian from "../../../shared/menuCategoriesImages/vegetarian.jpg";

const imageArray = [Fitness, FoodLover, Gym, Vegetarian];

export const MenuCategories = () => {
    const menuCategories = Object.keys(MenuCategory).filter(key => Number.isNaN(+key));

    return (
        <div className={styles.container}>
            {menuCategories.map((categoryKey, index) =>
                <Link className={styles.menuCategory} to={`menus/${categoryKey}`} key={categoryKey}>
                    <h4>{categoryKey}</h4>
                    <img className={styles.menuCategoryImage} src={imageArray[index]} alt={categoryKey}/>
                </Link> 
            )}
        </div>
    );
}