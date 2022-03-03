using System.Threading.Tasks;
using AccountsApi.V1.Domain;

namespace AccountsApi.V1.Gateways.Interfaces
{
    public interface ISnsGateway
    {
        Task Publish(AccountSns accountSns);

    }
}
