using System;
using System.ComponentModel.DataAnnotations;
using AccountsApi.V1.Boundary.BaseModel;
using AccountsApi.V1.Boundary.BaseModel.Interface;

namespace AccountsApi.V1.Boundary.Response
{
    public class AccountResponse : AccountResponseModel, IAccountModel
    {
        /// <example>
        ///     2021-03-29T15:10:37.471Z
        /// </example>
        public DateTime CreatedAt { get; set; }

        /// <example>
        ///     2021-03-29T15:10:37.471Z
        /// </example>
        public DateTime LastUpdatedAt { get; set; }

        /// <example>
        ///     Admin
        /// </example>
        [Required]
        public string CreatedBy { get; set; }
    }
}
