using System;
using System.Threading.Tasks;

namespace AccountsApi.V1.UseCase.Interfaces
{
    public interface IRemoveUseCase
    {
        public Task ExecuteAsync(Guid id);
    }
}
