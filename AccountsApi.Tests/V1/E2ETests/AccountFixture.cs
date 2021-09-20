using System;
using AccountsApi.V1.Boundary.Request;
using AccountsApi.V1.Boundary.Response;
using AccountsApi.V1.Infrastructure;
using Amazon.DynamoDBv2.DataModel;
using Amazon.SimpleNotificationService;
using AutoFixture;

namespace AccountsApi.Tests.V1.E2ETests
{
    public class AccountFixture : IDisposable
    {
        private readonly IDynamoDBContext _dbContext;
        private readonly IAmazonSimpleNotificationService _amazonSimpleNotificationService;

        public AccountDbEntity Account { get; private set; }

        public AccountRequest CreateAccountRequest { get; private set; }
        public AccountModel UpdateAccountRequest { get; private set; }

        public AccountFixture(IDynamoDBContext dbContext, IAmazonSimpleNotificationService amazonSimpleNotificationService)
        {
            _dbContext = dbContext;
            _amazonSimpleNotificationService = amazonSimpleNotificationService;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool _disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !_disposed)
            {
                if (null != Account)
                    _dbContext.DeleteAsync<AccountDbEntity>(Account.Id).GetAwaiter().GetResult();
                _disposed = true;
            }
        }
    }
}
