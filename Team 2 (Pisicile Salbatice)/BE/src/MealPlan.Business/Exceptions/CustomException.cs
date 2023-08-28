using System;

namespace MealPlan.Business.Exceptions
{
    public class CustomException
    {
        public int Code { get; internal set; }
        public string Message { get; internal set; }
    }
}

