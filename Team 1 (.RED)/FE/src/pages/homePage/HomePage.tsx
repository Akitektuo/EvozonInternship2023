import React from "react";
import { MenuCategories } from "./components/MenuCategories";
import styles from "./HomePage.module.scss";
import { Link } from "react-router-dom";

export const HomePage = () => (
    <div className={styles.container}>
        <h2>Choose your menu</h2>
        <MenuCategories />
        <p>Don't know what to choose from?</p>
        <Link to="/suggestedMenu">
            Get suggested menu
        </Link>
    </div>
);