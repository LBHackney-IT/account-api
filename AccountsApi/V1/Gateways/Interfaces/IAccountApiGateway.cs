using AccountsApi.V1.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountsApi.V1.Gateways.Interfaces
{
    public interface IAccountApiGateway
    {
        public Task<Account> GetByIdAsync(Guid id);
        public Task<IList<Account>> GetAllAsync(Guid targetId, AccountType accountType);
        public Task<IList<Account>> GetAllArrearsAsync(AccountType accountType, string sortBy, Direction direction);
        public Task AddAsync(Account account);
        public Task UpdateAsync(Account account);
        public Task<bool> AddBatchAsync(List<Account> accounts);
        Task<Account> GetByPrnAsync(string paymentReference);
    }
}
