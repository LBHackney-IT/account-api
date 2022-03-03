using AccountsApi.V1.Boundary.Request;
using AccountsApi.V1.Boundary.Response;
using AccountsApi.V1.Factories;
using AccountsApi.V1.Gateways;
using AccountsApi.V1.UseCase.Interfaces;
using System.Threading.Tasks;
using AccountsApi.V1.Gateways.Interfaces;

namespace AccountsApi.V1.UseCase
{
    public class GetAllArrearsUseCase : IGetAllArrearsUseCase
    {
        private readonly IAccountApiGateway _gateway;

        public GetAllArrearsUseCase(IAccountApiGateway gateway)
        {
            _gateway = gateway;
        }

        public async Task<AccountResponses> ExecuteAsync(ArrearRequest arrearRequest)
        {
            var response = await _gateway.GetAllArrearsAsync(arrearRequest.Type, arrearRequest.SortBy, arrearRequest.Direction)
                .ConfigureAwait(false);

            return new AccountResponses() { AccountResponseList = response.ToResponse() };
        }
    }
}
