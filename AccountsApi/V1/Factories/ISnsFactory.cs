using AccountsApi.V1.Boundary.Response;
using AccountsApi.V1.Domain;
using AccountsApi.V1.Infrastructure;

namespace AccountsApi.V1.Factories
{
    public interface ISnsFactory
    {
        AccountSns Create(Account account);

        AccountSns Update(UpdateEntityResult<AccountDbEntity> updateResult);
    }
}
