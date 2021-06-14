using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountApi.V1.Domain;
using AccountApi.V1.Factories;
using AccountApi.V1.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace AccountApi.V1.Gateways
{
    //TODO: Rename to match the data source that is being accessed in the gateway eg. MosaicGateway
    public class AccountApiGateway : IAccountApiGateway
    {
        private readonly AccountContext _accountDbContext;

        public AccountApiGateway(AccountContext accountDbContext)
        {
            _accountDbContext = accountDbContext;
        }

        public void Add(Account account)
        {
            _accountDbContext.Add(account);
        }

        public async Task AddAsync(Account account)
        {
            await _accountDbContext.AddAsync(account).ConfigureAwait(false);
        }

        public void AddRange(List<Account> accounts)
        {
            _accountDbContext.AddRange(accounts);
        }

        public async Task AddRangeAsync(List<Account> accounts)
        {
            await _accountDbContext.AddRangeAsync(accounts).ConfigureAwait(false);
        }

        public async Task<List<Account>> GetAllAsync(Guid targetId)
        {
            if (targetId == null)
                throw new ArgumentException("Invalid targetId");

            IQueryable<AccountDbEntity> data = _accountDbContext
                .AccountEntities
                .Where(p => p.TargetId == targetId);
            return await data.Select(p => p.ToDomain())
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public Account GetById(Guid id)
        {
            if (id == null)
                throw new ArgumentException("Invalid Id");
            return _accountDbContext.AccountEntities.Find(id)?.ToDomain();
        }

        public async Task<Account> GetByIdAsync(Guid id)
        {
            if (id == null)
                throw new ArgumentException("Invalid Id");
            var result= await _accountDbContext.AccountEntities.FindAsync(id).ConfigureAwait(false);
            return result?.ToDomain();
        }

        public void Remove(Account account)
        {
            _accountDbContext.Remove(account);
            _accountDbContext.SaveChanges();
        }

        public async Task RemoveAsync(Account account)
        {
            _accountDbContext.Remove(account);
            await _accountDbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public void RemoveRange(List<Account> accounts)
        {
            _accountDbContext.RemoveRange(accounts);
        }

        public async Task RemoveRangeAsync(List<Account> accounts)
        {
            foreach (Account account in accounts)
            {
                await RemoveAsync(account).ConfigureAwait(false);
            }
        }

        public void Update(Account account)
        {
            _accountDbContext.Entry<Account>(account).State = EntityState.Modified;
            _accountDbContext.SaveChanges();
        }

        public async Task UpdateAsync(Account account)
        {
            _accountDbContext.Entry<Account>(account).State = EntityState.Modified;
            await _accountDbContext.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
