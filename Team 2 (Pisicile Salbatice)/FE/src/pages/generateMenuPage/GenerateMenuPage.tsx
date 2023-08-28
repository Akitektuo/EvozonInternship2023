import { Button, MenuItem, Select, SelectChangeEvent, TextField } from "@mui/material"
import { observer } from "mobx-react"
import { ChangeEvent, useCallback, useContext } from "react";
import { GenerateMenuPageContext } from "./GenerateMenuPage.store";
import { CategoriesEnum } from "../../shared/CategoriesEnum";
import { useNavigate } from "react-router-dom";
import { RoutesConstants } from "../../shared/RoutesConstants";
import styles from "./GenerateMenuPage.module.scss";

export const GenerateMenuPage = observer(() => {
    const {
        menuType,
        priceSuggestion,
        setMenuType,
        setPriceSuggestion
    } = useContext(GenerateMenuPageContext);
    const navigate = useNavigate();

    const handleMenuTypeChange = useCallback((event: SelectChangeEvent<CategoriesEnum>) =>
        setMenuType(event.target.value as CategoriesEnum), [setMenuType]);

    const handlePriceChange = useCallback((event: ChangeEvent<HTMLInputElement>) =>
        setPriceSuggestion(Number(event.target.value)), [setPriceSuggestion]);

    const handleSubmitClick = useCallback(
        () => navigate(`${RoutesConstants.GeneratedMenusRoute}?menuType=${menuType}&priceSuggestion=${priceSuggestion}`),
        [navigate, menuType, priceSuggestion]);

    return <div className={styles.container}>
        <div className={styles.control}>
            <label>Menu Type</label>
            <Select value={menuType} onChange={handleMenuTypeChange}>
                <MenuItem value={CategoriesEnum.Fitness}>Fitness</MenuItem>
                <MenuItem value={CategoriesEnum.FoodLover}>Food Lover</MenuItem>
                <MenuItem value={CategoriesEnum.Gym}>Gym</MenuItem>
                <MenuItem value={CategoriesEnum.Vegetarian}>Vegetarian</MenuItem>
            </Select>
        </div>
        <div className={styles.control}>
            <label>Price Suggestion</label>
            <TextField type="number" value={isNaN(priceSuggestion) ? "" : priceSuggestion.toString()} onChange={handlePriceChange} />
        </div>
        <Button onClick={handleSubmitClick} variant="outlined">Submit</Button>
    </div>
});