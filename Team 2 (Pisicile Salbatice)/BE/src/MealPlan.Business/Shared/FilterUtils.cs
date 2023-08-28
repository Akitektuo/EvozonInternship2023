using MealPlan.Business.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace MealPlan.Business.Shared
{
    public static class FilterUtils
    {
        public static IQueryable<Thing> Filter<Thing>(this IQueryable<Thing> query, FiltrationModel filtrationModel)
        {
            filtrationModel = filtrationModel.GetNormalizedData();

            filtrationModel.ColumnClauses.IsValidColumnNames<Thing>();

            query = ApplyWhereConditions(query, filtrationModel);
            query = ApplySearchTextFiltering(query, filtrationModel);
            query = ApplyOrderByConditions(query, filtrationModel);

            return query;
        }

        private static FiltrationModel GetNormalizedData(this FiltrationModel model)
        {
            return new FiltrationModel
            {
                ColumnClauses = model.ColumnClauses.Select(x => new ColumnClauseModel { ColumnName = x.ColumnName.ToLower(), Value = x.Value }).ToList(),
                OrderByModels = model.OrderByModels.Select(x => new SortModel { Column = x.Column.ToLower(), SortOrder = x.SortOrder.ToLower() }).ToList(),
                SearchText = model.SearchText
            };
        }


        private static IQueryable<Thing> ApplyOrderByConditions<Thing>(IQueryable<Thing> query, FiltrationModel filtrationModel)
        {
            if (filtrationModel.OrderByModels.Count == 0)
                return query;

            var orderByConditions = filtrationModel.OrderByModels.Select(x => $"{x.Column} {x.SortOrder}").ToList();
            var orderBy = string.Join(",", orderByConditions.ToArray());

            return query.OrderBy(orderBy);
        }

        private static IQueryable<Thing> ApplySearchTextFiltering<Thing>(IQueryable<Thing> query, FiltrationModel filtrationModel)
        {
            if (string.IsNullOrEmpty(filtrationModel?.SearchText))
                return query;

            var entityProperties = typeof(Thing).GetProperties().Where(x => x.PropertyType == typeof(string));

            var lambdas = new List<Expression<Func<Thing, bool>>>();

            foreach (var prop in entityProperties)
            {
                var whereCondition = DynamicExpressionParser.ParseLambda<Thing, bool>(new ParsingConfig(), true, $"{prop.Name}.Contains(@0)", filtrationModel.SearchText);

                lambdas.Add(whereCondition);
            }

            return query.Where(lambdas.GeneratePredicateString(), lambdas.ToArray());
        }

        private static IQueryable<Thing> ApplyWhereConditions<Thing>(IQueryable<Thing> query, FiltrationModel filtrationModel)
        {
            if (filtrationModel.ColumnClauses.Count == 0)
                return query;

            var lambdas = new List<Expression<Func<Thing, bool>>>();

            foreach(var column in  filtrationModel.ColumnClauses)
            {
                var condition = DynamicExpressionParser.ParseLambda<Thing, bool>(new ParsingConfig(), true, $"o => o.{column.ColumnName} == @0", column.Value);

                lambdas.Add(condition);
            }

            return query.Where(lambdas.GeneratePredicateString(), lambdas.ToArray());
        }

        private static string GeneratePredicateString<Thing>(this List<Expression<Func<Thing, bool>>> lambdas)
        {
            var composedStringLambda = string.Empty;

            for (var i = 0; i < lambdas.Count; i++)
            {
                composedStringLambda += $"@{i}(it)";
                if (i != lambdas.Count - 1)
                {
                    composedStringLambda += " or ";
                }
            }

            return composedStringLambda;
        }

        private static void IsValidColumnNames<Thing>(this List<ColumnClauseModel> columnClauses)
        {
            var columnNames = columnClauses.Select(x => x.ColumnName).ToList();
            var entityColumnNames = typeof(Thing).GetProperties().Select(x => x.Name.ToLower()).ToList();

            var differentColumnNames = columnNames.Except(entityColumnNames).ToList();

            if (differentColumnNames.Count != 0)
                throw new CustomApplicationException(ErrorCode.InvalidColumnName, $"The following column names do not exist in the entity {string.Join(", ", differentColumnNames) }.");
        }
    }
}
