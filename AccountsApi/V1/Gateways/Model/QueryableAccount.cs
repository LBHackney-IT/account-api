using AccountsApi.V1.Domain;

namespace AccountsApi.V1.Gateways.Model
{
    public class QueryableAccount
    {
        public Account Create()
        {
            return new Account();
        }
    }
}
