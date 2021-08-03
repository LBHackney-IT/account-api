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
        private const string DEFAULT_SORT_FIELD = "LastUpdatedDate";

        /// <summary>
        /// Method for sort collection by field SortBy with type Direction
        /// </summary>
        /// <param name="accounts">Collection to sort</param>
        /// <param name="sortBy">Field in model to sort</param>
        /// <param name="direction">Type of sort [Asc, Desc]</param>
        public static IEnumerable<AccountDbEntity> Sort(this IEnumerable<AccountDbEntity> accounts, string sortBy, Direction direction)
        {
            if (string.IsNullOrWhiteSpace(sortBy))
            {
                sortBy = DEFAULT_SORT_FIELD;
            }

            var order = CreateOrderQuery<AccountDbEntity>(sortBy, direction.ToString());

            return accounts.AsQueryable().OrderBy(order).ToDynamicList<AccountDbEntity>();
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
