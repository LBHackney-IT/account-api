using System.Collections.Generic;
using AccountApi.V1.Domain;

namespace AccountApi.V1.Gateways
{
    public interface IExampleGateway
    {
        Account GetEntityById(int id);

        List<Account> GetAll();
    }
}
