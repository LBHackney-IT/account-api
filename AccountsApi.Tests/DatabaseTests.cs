using AccountsApi.V1.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using NUnit.Framework;

namespace AccountsApi.Tests
{
    [TestFixture]
    public class DatabaseTests
    {
        private IDbContextTransaction _transaction;
        protected AccountContext DatabaseContext { get; private set; }

        [SetUp]
        public void RunBeforeAnyTests()
        {
            var builder = new DbContextOptionsBuilder();
            builder.UseNpgsql(ConnectionString.TestDatabase());
            DatabaseContext = new AccountContext(builder.Options);

            DatabaseContext.Database.EnsureCreated();
            _transaction = DatabaseContext.Database.BeginTransaction();
        }

        [TearDown]
        public void RunAfterAnyTests()
        {
            _transaction.Rollback();
            _transaction.Dispose();
        }
    }
}
