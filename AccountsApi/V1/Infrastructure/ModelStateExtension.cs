using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AccountApi.V1.Infrastructure
{
    public static class ModelStateExtension
    {
        public static string GetErrorMessages(this ModelStateDictionary modelState)
        {
            return
                string.Join(",", modelState.SelectMany(e => e.Value.Errors.Select(s => s.ErrorMessage)));
        }
    }
}
