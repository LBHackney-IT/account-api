using System.Threading.Tasks;
using AccountsApi.V1.Domain;

namespace AccountsApi.V1.Gateways
{
    public interface ISnsGateway
    {
        Task Publish(AccountSns accountSns);

    }
}
