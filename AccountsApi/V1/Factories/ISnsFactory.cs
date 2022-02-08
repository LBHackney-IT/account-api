using AccountsApi.V1.Domain;

namespace AccountsApi.V1.Factories
{
    public interface ISnsFactory
    {
        AccountSns Create(Account account);

        AccountSns Update(Account account);
    }
}
