using AccountsApi.V1.Boundary.Response;
using AccountsApi.V1.Domain;
using AccountsApi.V1.Factories;
using AccountsApi.V1.Gateways;
using AccountsApi.V1.UseCase.Interfaces;
using System.Threading.Tasks;
using AccountsApi.V1.Boundary.Request;

namespace AccountsApi.V1.UseCase
{
    public class AddUseCase : IAddUseCase
    {
        private readonly IAccountApiGateway _gateway;

        public AddUseCase(IAccountApiGateway gateway)
        {
            _gateway = gateway;
        }

        public async Task<AccountResponseObject> ExecuteAsync(AccountRequestObject account)
        {
            Account _account = account.ToDomain();
            await _gateway.AddAsync(_account).ConfigureAwait(false);
            return _account.ToResponse();
        }
    }
}


/*

            if (charge == null)
            {
                throw new ArgumentNullException(nameof(charge));
            }

            var domainModel = charge.ToDomain();

            domainModel.Id = Guid.NewGuid();

            await _gateway.AddAsync(domainModel).ConfigureAwait(false);
            return domainModel.ToResponse();
 
 */
