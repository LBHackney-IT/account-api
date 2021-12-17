using System;
using AccountsApi.Tests.V1.E2ETests.Fixtures;
using AccountsApi.Tests.V1.E2ETests.Steps;
using TestStack.BDDfy;
using Xunit;

namespace AccountsApi.Tests.V1.E2ETests.Stories
{
    [Story(
        AsA = "Service",
        IWant = "The Account to be inserted",
        SoThat = "it is possible to search for transaction")]
    [Collection("Aws collection")]
    public class CreateAccountTests : IDisposable
    {
        private readonly AwsIntegrationTests<Startup> _dbFixture;
        private readonly AccountFixture _accountFixture;
        private readonly CreateAccountSteps _steps;
        public CreateAccountTests(AwsIntegrationTests<Startup> dbFixture)
        {
            _dbFixture = dbFixture;

            _accountFixture = new AccountFixture(dbFixture.DynamoDbContext);
            _steps = new CreateAccountSteps(dbFixture.Client);
        }

        [Fact]
        public void ServicePostWithValidInputReturnsInsertedDataWith200()
        {
            this.Given(g => _accountFixture.GiveANewAccountRequest())
                .When(w => _steps.WhenPostAccountCalled(_accountFixture.AccountRequest))
                .Then(t => _steps.ThenTHeLastResponseShouldBe201());
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /*private bool _disposed;*/

        protected virtual void Dispose(bool disposing)
        {
            /*if (disposing && !_disposed)
            {
                if (null != _transactionFixture)
                    _transactionFixture.Dispose();

                _disposed = true;
            }*/
        }

    }
}
