namespace MealPlan.Business.Exceptions
{
    public enum ErrorCode
    {
        BadCredentials = 100,
        UserAlreadyExists = 101,
        UnauthorizedAccess = 102,
        IngredientDoesNotExist = 200,
        IngredientAlreadyExists = 201,
        RecipeAlreadyExists = 300,
        RecipeNotFound = 301,
        RecipeAlredyUsed = 302,
        MealNameDuplicated = 400,
        MenuNameDuplicated = 500,
        MenuNotFound = 501,
        FailedMenuGeneration = 502,
        FailedMenuDeserialization = 503,
        OrderDoesNotExist = 600,
        CannotApproveOrder = 601,
        InvalidUserWallet = 602,
        CannotRejectOrder = 603,
        OrderIsOutdated = 604,
        CannotResetStatus = 605,
        AddOrderUserNotFound = 606,
        AddOrderMenuNotFound = 607,
        InvalidColumnName = 701
    }
}
