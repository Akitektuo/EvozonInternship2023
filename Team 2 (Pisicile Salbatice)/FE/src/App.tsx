import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";
import { NavigationBar } from "./navBar/NavigationBar";
import { HomePage } from "./pages/homePage/HomePage";
import { LoginPage } from "./pages/authenticationPages/loginPage/LoginPage";
import { RegisterPage } from "./pages/authenticationPages/registerPage/RegisterPage";
import { RoutesConstants } from "./shared/RoutesConstants";
import { observer } from "mobx-react";
import { useUserService } from "./services/userService/UseUserService";
import { useAppServices } from "./services/UseAppServices";
import { ServerErrorHandling } from "./pages/authenticationPages/components/serverErrorHandling/ServerErrorHandling";
import { RecipePage } from "./pages/recipePage/RecipePage";
import { ViewAllRecipesPage } from "./pages/viewAllRecipesPage/ViewAllRecipesPage";
import { AddRecipePage } from "./pages/addRecipePage/AddRecipePage";
import { MenuPage } from "./pages/menuPage/MenuPage";
import { RoleEnum } from "./pages/authenticationPages/loginPage/LoginPage.types";
import { CategoryPage } from "./pages/categoryPage/MenusByCategoryPage";
import { GenerateMenuPage } from "./pages/generateMenuPage/GenerateMenuPage";
import { GeneratedMenuPage } from "./pages/generatedMenuPage/GeneratedMenuPage";
import "./shared/Extensions";
import { NavigationBarContext } from "./navBar/Navigationbar.store";
import { useContext } from "react";
import "./App.scss"; 

export const App = observer(() => {
  useAppServices();
  const { userData, isAuthenticated, isUserInitialized } = useUserService();

  if (!isUserInitialized()) {
    return null;
  }

  const { heightNavigationBar } = useContext(NavigationBarContext);

  return (
    <BrowserRouter>
      <NavigationBar />
      <ServerErrorHandling />
      <div style={{
        height: `calc(100vh - 2px - ${heightNavigationBar}px)`}}>
        <Routes>
          <Route index element={<HomePage />} />
          {isAuthenticated() || (
            <>
              <Route path={RoutesConstants.LoginRoute} element={<LoginPage />} />
              <Route path={RoutesConstants.RegisterRoute} element={<RegisterPage />} />
            </>
          )}
        <Route path={`${RoutesConstants.RecipesRoute}?page:number`} element={<ViewAllRecipesPage />} />
        <Route path={`${RoutesConstants.RecipesRoute}/:recipeId`} element={<RecipePage />} />
        <Route path={`${RoutesConstants.MenusRoute}/:category`} element={<CategoryPage />} />
        <Route path={`${RoutesConstants.MenusRoute}/:category?page=:number`} element={<CategoryPage />} />
        <Route path={`${RoutesConstants.MenusRoute}/:category/details/:menuId`} element={<MenuPage />} />
          <Route path="*" element={<Navigate to={RoutesConstants.BaseRoute} />} />
          {userData?.roleId === RoleEnum.ADMIN && 
          <>
            <Route path={`${RoutesConstants.AddRecipeRoute}`} element={<AddRecipePage />} /> 
            <Route path={RoutesConstants.RecipesRoute} element={<ViewAllRecipesPage />} />
            <Route path={`${RoutesConstants.RecipesRoute}/:recipeId`} element={<RecipePage />} />
            <Route path={RoutesConstants.GenerateMenusRoute} element={<GenerateMenuPage />} />
            <Route path={RoutesConstants.GeneratedMenusRoute} element={<GeneratedMenuPage />} />
        </>}
        </Routes>
        </div>
    </BrowserRouter>
  );
});