using System;

namespace MealPlan.UnitTests.Utils
{
    public static class StringUtils
    {
        public static string RandomString(int length)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            string stringValue = "";
            var random = new Random();

            for (int i = 0; i < length; i++)
            {
                stringValue += chars[random.Next(chars.Length)];
            }

            return stringValue;
        }
    }
}