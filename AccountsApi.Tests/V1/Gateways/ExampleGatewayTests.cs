using AccountsApi.V1.Gateways;

namespace AccountsApi.Tests.V1.Gateways
{
    //TODO: Remove this file if Postgres gateway is not being used
    //TODO: Rename Tests to match gateway name
    //For instruction on how to run tests please see the wiki: https://github.com/LBHackney-IT/lbh-accounts-api/wiki/Running-the-test-suite. 
    public class ExampleGatewayTests : DatabaseTests
    {
        // private readonly Fixture _fixture = new Fixture();
        //private AccountApiGateway _classUnderTest;
 
        public   ExampleGatewayTests()
        {
            //_classUnderTest = new AccountApiGateway(DatabaseContext);
        }

        // [Test]
        // public void GetEntityByIdReturnsNullIfEntityDoesntExist()
        // {
        //     var response = _classUnderTest.GetEntityById(123);
        //
        //     response.Should().BeNull();
        // }
        //
        // [Test]
        // public void GetEntityByIdReturnsTheEntityIfItExists()
        // {
        //     var entity = _fixture.Create<Entity>();
        //     var databaseEntity = DatabaseEntityHelper.CreateDatabaseEntityFrom(entity);
        //
        //     DatabaseContext.DatabaseEntities.Add(databaseEntity);
        //     DatabaseContext.SaveChanges();
        //
        //     var response = _classUnderTest.GetEntityById(databaseEntity.Id);
        //
        //     databaseEntity.Id.Should().Be(response.Id);
        //     databaseEntity.CreatedAt.Should().BeSameDateAs(response.CreatedAt);
        // }

        //TODO: Add tests here for the get all method.
    }
}
