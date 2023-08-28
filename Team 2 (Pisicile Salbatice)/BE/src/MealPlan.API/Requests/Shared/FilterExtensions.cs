using MealPlan.Business.Shared;
using System.Collections.Generic;
using System.Linq;
using BusinessFiltrationModel = MealPlan.Business.Shared.FiltrationModel;
using BusinessColumnClauseModel = MealPlan.Business.Shared.ColumnClauseModel;

namespace MealPlan.API.Requests.Shared
{
    public static class FilterExtensions
    {
        public static BusinessFiltrationModel ToBusinessFiltrationModel(this FiltrationModel filtrationModel)
        {
            return new BusinessFiltrationModel
            {
                ColumnClauses = filtrationModel.ColumnClauses.ToBusinessColumnClauses().ToList(),
                OrderByModels = filtrationModel.OrderByModels.ToSortModels().ToList(),
                SearchText = filtrationModel.SearchText
            };
        }

        public static IEnumerable<SortModel> ToSortModels(this IEnumerable<OrderByModel> orderByModels)
        {
            return orderByModels.Select(x => new SortModel
            {
                Column = x.Column,
                SortOrder = x.SortOrder
            });
        }

        public static IEnumerable<BusinessColumnClauseModel> ToBusinessColumnClauses(this IEnumerable<ColumnClauseModel> orderByModels)
        {
            return orderByModels.Select(x => new BusinessColumnClauseModel
            {
                ColumnName = x.ColumnName,
                Value = x.Value
            });
        }
    }
}
