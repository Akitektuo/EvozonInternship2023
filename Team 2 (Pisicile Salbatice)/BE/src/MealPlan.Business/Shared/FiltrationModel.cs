using System.Collections.Generic;

namespace MealPlan.Business.Shared
{
    public class FiltrationModel
    {
        public List<ColumnClauseModel> ColumnClauses { get; set; }
        public string SearchText { get; set; }
        public List<SortModel> OrderByModels { get; set; }
    }
}
