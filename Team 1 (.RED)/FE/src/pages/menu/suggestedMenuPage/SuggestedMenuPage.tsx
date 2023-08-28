import { useContext } from "react";
import { getSuggestedMenuContext } from "./SuggestedMenuPage.store";
import { observer } from "mobx-react";
import { FormControlLabel, Checkbox, Button, Box, CircularProgress } from "@mui/material";
import { InputComponent } from "../../components/inputComponent/InputComponent";
import { MenuCategory } from "../../../shared/Types";
import { SuggestedMenuDetails } from "./components/SuggestedMenuDetails";
import styles from "./SuggestedMenuPage.module.scss";

export const SuggestedMenuPage = observer(() => {
    const { 
        setPriceSuggestion,
        setCategory,
        getSuggestedMenu,
        suggestedMenu,
        saveSuggestedMenu,
        categoryAndPrice,
        isLoading
    } = useContext(getSuggestedMenuContext);

    return (
        <Box className={styles.container}>
            <h3>Get a Menu Suggestion</h3>
            <Box className={styles.formContainer}>
                <Box className={styles.inputContainer}>
                    <h4>Price Suggestion:</h4>
                    <input  className={styles.input} type="number" min="0" onChange={event => setPriceSuggestion(event.target.value)} value={categoryAndPrice.priceSuggestion.toString()}/>
                </Box>
                <Box className={styles.checkboxContainer}>
                    <h4 className={styles.title}>Menu Category:</h4>
                    <FormControlLabel 
                        control={<Checkbox checked={categoryAndPrice.categoryId === MenuCategory.Fitness} 
                        onChange={event => event.target.checked && setCategory(MenuCategory.Fitness)}/>} 
                        label="Fitness"
                    />
                    <FormControlLabel 
                        control={<Checkbox checked={categoryAndPrice.categoryId === MenuCategory.FoodLover} 
                        onChange={event => event.target.checked && setCategory(MenuCategory.FoodLover)}/>} 
                        label="FoodLover"
                    />
                    <FormControlLabel 
                        control={<Checkbox checked={categoryAndPrice.categoryId === MenuCategory.Gym} 
                        onChange={event => event.target.checked && setCategory(MenuCategory.Gym)}/>} 
                        label="Gym"
                    />
                    <FormControlLabel 
                        control={<Checkbox checked={categoryAndPrice.categoryId === MenuCategory.Vegetarian} 
                        onChange={event => event.target.checked && setCategory(MenuCategory.Vegetarian)}/>} 
                        label="Vegetarian"
                    />
                </Box>
                <Button onClick={getSuggestedMenu} className={styles.submitButton}>Submit</Button>
            </Box>

            {isLoading && 
                <Box className={styles.suggestedMenuContainer}>
                    <CircularProgress />
                    <p>Loading</p>
                </Box>
            }

            {suggestedMenu && (
                <Box className={styles.suggestedMenuContainer}>
                    <SuggestedMenuDetails suggestedMenu={suggestedMenu} />
                    <Button onClick={saveSuggestedMenu} className={styles.saveButton}>Save</Button>
                </Box>
            )}
        </Box>
    )
});