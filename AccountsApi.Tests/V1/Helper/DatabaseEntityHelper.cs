using AutoFixture;
using AccountsApi.V1.Domain;
using AccountsApi.V1.Infrastructure;

namespace AccountsApi.Tests.V1.Helper
{
    public static class DatabaseEntityHelper
    {
        public static AccountDbEntity CreateDatabaseEntity()
        {
            var entity = new Fixture().Create<AccountDbEntity>();

            return CreateDatabaseEntityFrom(entity);
        }

        public static AccountDbEntity CreateDatabaseEntityFrom(AccountDbEntity entity)
        {
            return new AccountDbEntity
            {
                Id = entity.Id,
                StartDate = entity.StartDate
            };
        }
    }
}
