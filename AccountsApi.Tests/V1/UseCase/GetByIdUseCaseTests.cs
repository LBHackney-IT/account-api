using AccountsApi.V1.Gateways;
using AccountsApi.V1.UseCase;
using Moq; 

namespace AccountsApi.Tests.V1.UseCase
{
    public class GetByIdUseCaseTests
    {
        private Mock<IAccountApiGateway> _mockGateway;
        private GetByIdUseCase _classUnderTest;

 
        public   GetByIdUseCaseTests()
        {
            _mockGateway = new Mock<IAccountApiGateway>();
            _classUnderTest = new GetByIdUseCase(_mockGateway.Object);
        }

        //TODO: test to check that the use case retrieves the correct record from the database.
        //Guidance on unit testing and example of mocking can be found here https://github.com/LBHackney-IT/lbh-accounts-api/wiki/Writing-Unit-Tests
    }
}
