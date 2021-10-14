using AccountsApi.V1.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text;

namespace AccountsApi.V1.Infrastructure
{
    /// <summary>
    /// Extensions made by Hanna Holosova
    /// This class implements sort by dynamic field
    /// </summary>
    public static class OrderExtensions
    {
        /// <summary>
        /// Default property to sort
        /// </summary>
        private const string DEFAULT_SORT_FIELD = "LastUpdatedAt";

        /// <summary>
        /// Method for sort collection by field SortBy with type Direction
        /// </summary>
        /// <param name="models">Collection to sort</param>
        /// <param name="sortBy">Field in model to sort</param>
        /// <param name="direction">Type of sort [Asc, Desc]</param>
        public static IEnumerable<T> Sort<T>(this IEnumerable<T> models, string sortBy, Direction direction)
        {
            Type entityType = models.GetType().GetGenericArguments().Single();

            if (string.IsNullOrWhiteSpace(sortBy))
            {
                sortBy = DEFAULT_SORT_FIELD;
            }

            var order = CreateOrderQuery<T>(sortBy, direction.ToString());

            return models.AsQueryable().OrderBy(order).ToDynamicList<T>();
        }

        /// <summary>
        /// Support method, which return correct string for dynamic LINQ OrderBy
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="sortBy">Field in model to sort</param>
        /// <param name="direction">Type of sort [Asc, Desc]</param>
        private static string CreateOrderQuery<T>(string sortBy, string direction)
        {
            var propertyInfos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var orderQueryBuilder = new StringBuilder();

            var objectProperty = propertyInfos.FirstOrDefault(_ => _.Name.Equals(sortBy, StringComparison.InvariantCultureIgnoreCase));

            var directionQuery = direction.Equals("asc", StringComparison.InvariantCultureIgnoreCase) ? "ascending" : "descending";

            orderQueryBuilder.Append($"{objectProperty?.Name ?? DEFAULT_SORT_FIELD} {directionQuery}");

            return orderQueryBuilder.ToString();
        }
    }
}
