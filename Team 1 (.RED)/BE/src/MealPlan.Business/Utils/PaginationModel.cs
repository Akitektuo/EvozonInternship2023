using System.Collections.Generic;

namespace MealPlan.Business.Utils
{
    public class PaginationModel<T>
    {
        public List<T> Items { get; set; }
        public int TotalRecords { get; set; }
    }
}