using System.Collections.Generic;
using System.Linq;
using AccountApi.V1.Boundary.Response;
using AccountApi.V1.Domain;

namespace AccountApi.V1.Factories
{
    public static class ResponseFactory
    {
        //TODO: Map the fields in the domain object(s) to fields in the response object(s).
        // More information on this can be found here https://github.com/LBHackney-IT/lbh-base-api/wiki/Factory-object-mappings
        public static AccountResponseObject ToResponse(this Account domain)
        {
            return new AccountResponseObject();
        }

        public static List<AccountResponseObject> ToResponse(this IEnumerable<Account> domainList)
        {
            return domainList.Select(domain => domain.ToResponse()).ToList();
        }
    }
}
