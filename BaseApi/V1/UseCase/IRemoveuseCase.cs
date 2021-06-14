using System;
using System.Threading.Tasks;

namespace AccountApi.V1.UseCase
{
    public interface IRemoveuseCase
    {
        public void Execute(Guid id);
        public Task ExecuteAsync(Guid id);
    }
}
