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

        [LogCall]
        public async Task AddAsync(Account account)
        {
            _logger.LogDebug($"Calling AccountApiGateway.AddAsync for account ID: {account.Id}");
            await _accountDbContext.AddAsync(account).ConfigureAwait(false);
        }

        [LogCall]
        public async Task<List<Account>> GetAllArrearsAsync(AccountType accountType, string sortBy, Direction direction)
        {
            _logger.LogDebug($"Calling AccountApiGateway.GetAllArrearsAsync for accountType: {accountType}, sortBy: {sortBy} and direction: {direction}");
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
            _logger.LogDebug($"Calling AccountApiGateway.GetAllAsync for targetId: {targetId} and accountType: {accountType}");

            var data = _accountDbContext
                .AccountEntities
                .Where(p => p.TargetId == targetId);
            return await data.Select(p => p.ToDomain())
                .ToListAsync()
                .ConfigureAwait(false);
        }

        [LogCall]
        public async Task<Account> GetByIdAsync(Guid id)
        {
            _logger.LogDebug($"Calling AccountApiGateway.GetByIdAsync for ID: {id}");
            if (id == null)
                throw new ArgumentException("Invalid Id");
            var result = await _accountDbContext.AccountEntities.FindAsync(id).ConfigureAwait(false);
            return result?.ToDomain();
        }

        [LogCall]
        public async Task RemoveAsync(Account account)
        {
            _logger.LogDebug($"Calling AccountApiGateway.RemoveAsync for account ID: {account.Id}");
            _accountDbContext.Remove(account);
            await _accountDbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        [LogCall]
        public async Task UpdateAsync(Account account)
        {
            _logger.LogDebug($"Calling AccountApiGateway.UpdateAsync for account ID: {account.Id}");
            _accountDbContext.Entry<Account>(account).State = EntityState.Modified;
            await _accountDbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        [LogCall]
        public Task<bool> AddBatchAsync(List<Account> accounts)
        {
            throw new NotImplementedException();
        }
    }
}
