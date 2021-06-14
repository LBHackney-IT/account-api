using AccountApi.V1.Boundary.Response;
using AccountApi.V1.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountApi.V1.UseCase.Interfaces
{
    public interface IRemoveUseCase
    {
        public void Execute(Guid id);
        public Task ExecuteAsync(Guid id);
    }
}
