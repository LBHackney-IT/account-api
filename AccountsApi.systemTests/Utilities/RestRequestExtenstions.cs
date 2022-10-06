using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsApi.systemTests.Utilities
{
    public static class RestRequestExtenstions
    {
        public static async Task<RestResponse> TryGetResponse(
            this RestClient restClient,
            Func<RestClient, Task<RestResponse>> restFunction,
            int retryAttempts = 5,
            int waitSecondsBetweenTries = 30)
        {
            for(var attemptNumber =1; attemptNumber <=retryAttempts; attemptNumber++)
            {
                try
                {
                    var response = await restFunction(restClient);
                    return response;
                }
                catch
                {
                    if (attemptNumber == retryAttempts)
                    {
                        throw;
                    }
                    System.Threading.Thread.Sleep(1000*waitSecondsBetweenTries);
                }
            }
            throw new NotSupportedException("Unable to make rest request");

        }
    }
}
