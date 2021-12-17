using AccountsApi.V1.Boundary.Request;
using Amazon.DynamoDBv2.DataModel;
using AutoFixture;

namespace AccountsApi.Tests.V1.E2ETests.Fixtures
{
    public class AccountFixture
    {
        private readonly IDynamoDBContext _dbContext;

        public AccountFixture(IDynamoDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        private readonly Fixture _fixture=new Fixture();
        public AccountRequest AccountRequest { get; private set; }=new AccountRequest();

        public void GiveANewAccountRequest()
        {
            AccountRequest = _fixture.Create<AccountRequest>();
        }
    }
}
