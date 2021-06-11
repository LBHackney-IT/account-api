using AccountApi.V1.Domain;
using AccountApi.V1.Infrastructure;

namespace AccountApi.V1.Factories
{
    public static class EntityFactory
    {
        public static Account ToDomain(this AccountDbEntity databaseEntity)
        {
            //TODO: Map the rest of the fields in the domain object.
            // More information on this can be found here https://github.com/LBHackney-IT/lbh-base-api/wiki/Factory-object-mappings

            return new Account
            {
                Id = databaseEntity.Id,
                CreatedAt = databaseEntity.CreatedAt
            };
        }

        public static AccountDbEntity ToDatabase(this Account entity)
        {
            //TODO: Map the rest of the fields in the database object.

            return new AccountDbEntity
            {
                Id = entity.Id,
                CreatedAt = entity.CreatedAt
            };
        }
    }
}
