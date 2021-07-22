using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AccountsApi.V1.Domain;

namespace AccountsApi.V1.Gateways
{
    public interface IAccountApiGateway
    {
        public Task<Account> GetByIdAsync(Guid id);
        public Task<List<Account>> GetAllAsync(Guid targetId, AccountType accountType);

        public Task AddAsync(Account account);
        public Task RemoveAsync(Account account);
        public Task UpdateAsync(Account account);
    }
}
