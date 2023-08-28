namespace MealPlan.API.Requests.Shared
{
    public static class RegexConstants
    {
        public static string Email { get; } = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
        public static string Name { get; } = @"^(?!.*\s$)(?!^\s)(?!.*\s\\s)(?!.*\s\s\s)[A-Za-z]+(?:[-'\s]?[A-Za-z]+)*$";
        public static string Password { get; } = @"^(?!\s)(?!.*\s$)(?=.*\S)[\S\s]*$";
    }
}
