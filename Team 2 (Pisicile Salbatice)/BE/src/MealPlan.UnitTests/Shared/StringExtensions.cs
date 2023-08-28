using System.Text;

namespace MealPlan.UnitTests.Shared
{
    public static class StringExtensions
    {
        public static string StringOfLength(int length, char c = 'a')
        {
            var stringBuilder = new StringBuilder();
            for(int i = 0; i < length; i++)
            {
                stringBuilder.Append(c);
            }
            return stringBuilder.ToString();
        }
    }
}
