using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AccountApi.V1.Domain;

namespace AccountApi.V1.Gateways
{
    public interface IAccountApiGateway
    {
        public Account GetById(Guid id);
        public Task<Account> GetByIdAsync(Guid id);
        public Task<List<Account>> GetAllAsync(Guid targetId);

        public void Add(Account account);
        public Task AddAsync(Account account);
        public void AddRange(List<Account> accounts);
        public Task AddRangeAsync(List<Account> accounts);

        public void Remove(Account account);
        public Task RemoveAsync(Account account);
        public void RemoveRange(List<Account> accounts);
        public Task RemoveRangeAsync(List<Account> accounts);

        public void Update(Account account);
        public Task UpdateAsync(Account account);
    }
}
