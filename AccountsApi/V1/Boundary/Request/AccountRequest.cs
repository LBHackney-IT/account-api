using System.ComponentModel.DataAnnotations;
using AccountsApi.V1.Boundary.BaseModel;
using AccountsApi.V1.Boundary.BaseModel.Interface;

namespace AccountsApi.V1.Boundary.Request
{
    public class AccountRequest : AccountBaseModel, IAccountModel
    {
        /// <example>
        ///     Admin
        /// </example>
        [Required]
        public string CreatedBy { get; set; }
    }
}
