using MealPlan.Business.Exceptions;
using MealPlan.Business.Orders.Models;
using MealPlan.Business.Utils;
using MealPlan.Data.Models.Orders;
using Microsoft.AspNetCore.Authentication;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace MealPlan.Business.Orders
{
    public static class OrderExtensions
    {
        public static IQueryable<OrderDetails> ToOrderDetails(this IQueryable<Order> query)
        {
            return query.Select(q => new OrderDetails
            {
                UserEmail = q.User.Email,
                MenuName = q.Menu.Name,
                StartDate = q.StartDate,
                EndDate = q.EndDate,
                OrderStatus = q.OrderStatusId.ToString(),
            });
        }

        public static IQueryable<Order> FilterByStatus(this IQueryable<Order> orders, OrderStatus status)
        {
            return !status.Equals((OrderStatus)0) ? orders.Where(o => o.OrderStatusId == status) : orders;
        }

        public static Dictionary<string, KeyValuePair<string, string>> CreateDictionary()
        {
            var propertiesMapping = new Dictionary<string, KeyValuePair<string, string>>()
            {
                {"UserEmail", new KeyValuePair<string, string>("User.Email", OrderConstants.StringType)},
                {"MenuName", new KeyValuePair<string, string>("Menu.Name", OrderConstants.StringType)},
                {"StartDate", new KeyValuePair<string, string>("StartDate", OrderConstants.DateTimeType)},
                {"EndDate", new KeyValuePair<string, string>("EndDate", OrderConstants.DateTimeType)},
                {"OrderStatus", new KeyValuePair<string, string>("OrderStatusId", OrderConstants.OrderStatusType)}
            };

            return propertiesMapping;
        }

        public static IQueryable<Order> OrderByGivenProperties(this IQueryable<Order> orders, string properties, bool orderByDescending)
        {
            var propertiesMapping = CreateDictionary();
            var propertiesForOrdering = "";

            if (properties.Equals(""))
            {
                propertiesForOrdering = "StartDate " + (orderByDescending ? "desc" : "asc");
            }

            foreach (var property in propertiesMapping)
            {
                if (properties.Contains(property.Key))
                {
                    string sorting = orderByDescending ? "desc" : "";
                    var orderingColumn = property.Value.Key + " " + sorting;

                    if (propertiesForOrdering.Equals(""))
                    {
                        propertiesForOrdering = orderingColumn;
                    }
                    else
                    {
                        propertiesForOrdering = propertiesForOrdering + "," + orderingColumn;
                    }
                }
            }

            if (propertiesForOrdering.Equals(""))
            {
                throw new CustomApplicationException(ErrorCode.OrderingPropertyNotFound, "Ordering property was not found");
            }

            return orders.OrderBy(propertiesForOrdering);
        }

        public static IQueryable<Order> FilterBySearchText(this IQueryable<Order> orders, string searchText)
        {
            var propertiesMapping = CreateDictionary();
            string filteringString = "";

            if (searchText == null || searchText.Length <= OrderConstants.MinLengthOfSearchText)
            {
                return orders;
            }

            foreach (var property in propertiesMapping)
            {
                if (property.Value.Value == OrderConstants.StringType)
                {
                    if (filteringString.Equals(""))
                    {
                        filteringString = $"{property.Value.Key}.Contains(@0)";
                    }
                    else
                        filteringString = filteringString + " or " + $"{property.Value.Key}.Contains(@0)";
                }
            }

            return orders.Where(filteringString, searchText);
        }

        public static IQueryable<Order> DataProcessing(this IQueryable<Order> orders, OrderStatus status, string searchText, string properties, bool orderByDescending)
        {
            return orders.FilterByStatus(status)
                .FilterBySearchText(searchText)
                .OrderByGivenProperties(properties, orderByDescending);
        }
    }
}