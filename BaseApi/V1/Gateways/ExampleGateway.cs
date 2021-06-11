using System.Collections.Generic;
using AccountApi.V1.Domain;
using AccountApi.V1.Factories;
using AccountApi.V1.Infrastructure;

namespace AccountApi.V1.Gateways
{
    //TODO: Rename to match the data source that is being accessed in the gateway eg. MosaicGateway
    public class ExampleGateway : IExampleGateway
    {
        private readonly DatabaseContext _databaseContext;

        public ExampleGateway(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public Account GetEntityById(int id)
        {
            var result = _databaseContext.DatabaseEntities.Find(id);

            return result?.ToDomain();
        }

        public List<Account> GetAll()
        {
            return new List<Account>();
        }
    }
}
