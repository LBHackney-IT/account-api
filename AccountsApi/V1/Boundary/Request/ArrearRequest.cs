using AccountsApi.V1.Domain;
using AccountsApi.V1.Infrastructure;
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
        [System.ComponentModel.DataAnnotations.AllowedValues(typeof(AccountType))]
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
        [System.ComponentModel.DataAnnotations.AllowedValues(typeof(Direction))]
        public Direction Direction { get; set; }
    }
}
