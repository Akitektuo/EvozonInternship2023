﻿using MealPlan.Business.Versions.Models;
using MealPlan.Data.Models.ApplicationVersions;
using System.Linq;

namespace MealPlan.Business.Versions
{
    public static class VersionExtensions
    {
        public static IQueryable<VersionCode> ToVersionCode(this IQueryable<ApplicationVersion> query)
        {
            return query.Select(q => new VersionCode
            {
                Version = q.Version
            });
        }
    }
}
