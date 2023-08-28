import React, { useContext } from "react";
import "./App.scss";
import { ThemeProvider, createTheme } from "@mui/material";
import { observer } from "mobx-react";
import { Routes, Route, BrowserRouter } from "react-router-dom";
import { NavigationBar } from "./pages/components/navigationBar/NavigationBar";
import { HomePage } from "./pages/homePage/HomePage";
import { LoginPage } from "./pages/loginPage/LoginPage";
import { RegisterPage } from "./pages/registerPage/RegisterPage";
import { userDetailsContext } from "./services/UserDetails.store";
import { PopUpNotification } from "./services/popUpNotifications/PopUpNotifications";
import { RecipesPage } from "./pages/recipesPage/AllRecipesPage";
import { RecipePage } from "./pages/recipePage/RecipePage";
import { UnauthorizedPage } from "./pages/unauthorizedPage/UnauthorizedPage";
import { AddRecipePage } from "./pages/addRecipePage/AddRecipePage";
import { MenuDetailsPage } from "./pages/menu/menuDetailsPage/MenuDetailsPage";
import { MenusPage } from "./pages/menu/menusPage/MenusPage";
import { SuggestedMenuPage } from "./pages/menu/suggestedMenuPage/SuggestedMenuPage";

export const App = observer(() => {
    const { isLoggedIn, userDetails, isAdmin } = useContext(userDetailsContext);

    const theme = createTheme({
        typography: {
            fontFamily: "'Rubik', sans-serif",
        }
    });

    return (
        <ThemeProvider theme={theme}>
            <PopUpNotification />
            <BrowserRouter>
                <Routes>
                    <Route path="/" element={<NavigationBar />}>
                        <Route index element={<HomePage />} />
                        <Route index element={<HomePage />} />
                        {isLoggedIn() ? (
                            <>
                                <Route path="/login-page" element={<p>You are already logged in as {userDetails?.firstName} {userDetails?.lastName}</p>} />
                                <Route path="menus/:menuCategory" element={<MenusPage />} />
                                <Route path="menus/:menuCategory/:id" element={<MenuDetailsPage />} />
                                <Route path="/suggestedMenu" element={<SuggestedMenuPage />} />
                            </>
                            ) : (
                            <>
                                <Route path="/login-page" element={<LoginPage />} /> 
                                <Route path="/register-page" element={<RegisterPage />} />
                                <Route path="*" element={<UnauthorizedPage />} />
                            </>
                        )}
                        {isAdmin() ? (
                            <>
                                <Route path="recipes" element={<RecipesPage />} />
                                <Route path="recipe/:id" element={<RecipePage />} />
                                <Route path="recipes/createRecipe" element={<AddRecipePage />} />
                            </>
                            ) : <Route path="*" element={<UnauthorizedPage />} />
                        }
                    </Route>
                </Routes>
            </BrowserRouter>
        </ThemeProvider>
    );
});
