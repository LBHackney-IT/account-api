using AccountApi.V1.Boundary.Response;

namespace AccountApi.V1.UseCase.Interfaces
{
    public interface IGetByIdUseCase
    {
        AccountResponseObject Execute(int id);
    }
}
