namespace MealPlan.API.Utils
{
    public static class RegexConstants
    {
        public static readonly string Email = "^\\s*[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}\\s*$";
        public static readonly string PasswordNoTrailingSpaces = "^[^\\s]*$";
        public static readonly string Name = "^[a-zA-Z\\s]+$";
        public static readonly string PhoneNumber = "^[\\+]?[(]?[0-9]{3}[)]?[-\\s\\.]?[0-9]{3}[-\\s\\.]?[0-9]{4,6}$";
    }
}