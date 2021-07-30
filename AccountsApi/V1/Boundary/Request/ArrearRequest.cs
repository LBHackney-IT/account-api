using AccountApi.V1.Infrastructure;
using AccountsApi.V1.Domain;
using System.ComponentModel.DataAnnotations;

namespace AccountsApi.V1.Boundary.Request
{
    public class ArrearRequest
    {
        /// <summary>
        /// Type of account [Master, Recharge, Sundry]
        /// </summary>
        /// <example>
        /// Master
        /// </example>
        [Required]
        [AllowedValues(AccountType.Master, AccountType.Recharge, AccountType.Sundry)]
        public AccountType Type { get; set; }

        /// <summary>
        /// Name of property by which sort
        /// </summary>
        /// <example>
        /// AccountBalance
        /// </example>
        public string SortBy { get; set; }

        /// <summary>
        /// Ascending or descending sort [Asc, Desc]
        /// </summary>
        /// <example>
        /// Asc
        /// </example>
        [AllowedValues(Direction.Asc, Direction.Desc)]
        public Direction Direction { get; set; }
    }
}
