using AccountApi.V1.Boundary.Response;

namespace AccountApi.V1.UseCase.Interfaces
{
    public interface IGetAllUseCase
    {
        ResponseObjectList Execute();
    }
}
