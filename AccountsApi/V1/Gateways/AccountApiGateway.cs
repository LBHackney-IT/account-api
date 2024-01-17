using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountsApi.V1.Domain;
using AccountsApi.V1.Factories;
using AccountsApi.V1.Gateways.Interfaces;
using AccountsApi.V1.Infrastructure;
using Hackney.Core.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AccountsApi.V1.Gateways
{
    public class AccountApiGateway : IAccountApiGateway
    {
        private readonly AccountContext _accountDbContext;
        private readonly ILogger<AccountApiGateway> _logger;

        public AccountApiGateway(AccountContext accountDbContext, ILogger<AccountApiGateway> logger)
        {
            _accountDbContext = accountDbContext;
            _logger = logger;
        }

        public async Task AddAsync(Account account)
        {
            await _accountDbContext.AddAsync(account).ConfigureAwait(false);
        }

        public async Task<List<Account>> GetAllArrearsAsync(AccountType accountType, string sortBy, Direction direction)
        {
            var data = _accountDbContext
                .AccountEntities
                .Where(x => x.AccountType == accountType);

            return await data.ToList().Sort<AccountDbEntity>(sortBy, direction)
                .Select(p => p.ToDomain()).AsQueryable()
                .ToListAsync().ConfigureAwait(false);
        }

        [LogCall]
        public async Task<List<Account>> GetAllAsync(Guid targetId, AccountType accountType)
        {
            _logger.LogDebug($"Calling AccountApiGateway.GetAllAsync for targetId {targetId} and accountType {accountType}");

            var data = _accountDbContext
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
        public Task<bool> AddBatchAsync(List<Account> accounts)
        {
            throw new NotImplementedException();
        }
    }
}
