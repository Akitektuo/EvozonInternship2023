namespace MealPlan.Business.Exceptions
{
    public enum ErrorCode
    {
        InvalidCredentials = 101,
        RegistrationEmailAlreadyUsed = 102,

        RecipeNotFound = 201,
        IngredientNotFound = 202,
        EmptyIngredientsList = 203,
        IngredientAlreadyExist = 204,

        MealTypeNotFound = 301,
        MealRecipeNotFound = 302,
        MealRecipeAlreadyUsed = 303,

        MenuNotFound = 401,
        MenuMealTypeDuplicated = 403,
        MenuCategoryNotFound = 404,

        UnsuccessfulMenuDeserialization = 501,
        UnsuccessfulMenuGeneration = 502,

        OrderUserNotFound = 601,
        OrderMenuNotFound = 602,

        OrderDoesNotExist = 701,
        OrderAlreadyApproved = 702,
        UserDoesNotHaveEnoughMoney = 703,
        OrderStartDatePassed = 704,

        OrderAlreadyDenied = 801,

        OrderingPropertyNotFound = 901
    }
}