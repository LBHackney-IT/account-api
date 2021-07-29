using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountsApi.V1.Domain;
using AccountsApi.V1.Factories;
using AccountsApi.V1.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace AccountsApi.V1.Gateways
{
    public class AccountApiGateway : IAccountApiGateway
    {
        private readonly AccountContext _accountDbContext;

        public AccountApiGateway(AccountContext accountDbContext)
        {
            _accountDbContext = accountDbContext;
        }

        public async Task AddAsync(Account account)
        {
            await _accountDbContext.AddAsync(account).ConfigureAwait(false);
        }

        public async Task<List<Account>> GetAllAsync(Guid targetId, AccountType accountType)
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

        public async Task<Account> GetByIdAsync(Guid id)
        {
            if (id == null)
                throw new ArgumentException("Invalid Id");
            var result = await _accountDbContext.AccountEntities.FindAsync(id).ConfigureAwait(false);
            return result?.ToDomain();
        }

        public async Task RemoveAsync(Account account)
        {
            _accountDbContext.Remove(account);
            await _accountDbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task UpdateAsync(Account account)
        {
            _accountDbContext.Entry<Account>(account).State = EntityState.Modified;
            await _accountDbContext.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
