using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AccountsApi.V1.Domain;

namespace AccountsApi.V1.Gateways
{
    public interface IAccountApiGateway
    {
        public Task<Account> GetByIdAsync(Guid id);
        public Task<List<Account>> GetAllAsync(Guid targetId,string accountType);

        public void Add(Account account);
        public Task AddAsync(Account account);
        public void AddRange(List<Account> accounts);
        public Task AddRangeAsync(List<Account> accounts);

        public Task RemoveAsync(Account account);
        public Task RemoveRangeAsync(List<Account> accounts);

        public void Update(Account account);
        public Task UpdateAsync(Account account);
    }
}
