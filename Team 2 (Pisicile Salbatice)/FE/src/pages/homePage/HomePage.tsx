import styles from "./HomePage.module.scss";
import { CategoryCard } from "./components/CategoryCard";
import { observer } from "mobx-react";
import { RoutesConstants } from "../../shared/RoutesConstants";
import { CategoriesEnum } from "../../shared/CategoriesEnum";

export const HomePage = observer(() => {
    const listOfCategoriesEnum = Object.keys(CategoriesEnum).filter((value) => isNaN(Number(value)));
    
    return (
        <div className={styles.homePageContainer}>
            {listOfCategoriesEnum.map((category) => (
                <CategoryCard 
                    key={category} 
                    title={category} 
                    path={`${RoutesConstants.MenusRoute}/${category}?page=1`} />
            ))}
        </div>
    )
});    