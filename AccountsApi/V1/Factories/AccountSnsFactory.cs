using System;
using AccountsApi.V1.Domain;
using AccountsApi.V1.Infrastructure;

namespace AccountsApi.V1.Factories
{
    public class AccountSnsFactory : ISnsFactory
    {
        public AccountSns Create(Account account)
        {
            return new AccountSns
            {
                CorrelationId = Guid.NewGuid(),
                DateTime = DateTime.UtcNow,
                EntityId = account.Id,
                Id = Guid.NewGuid(),
                EventType = CreateEventConstants.EVENTTYPE,
                Version = CreateEventConstants.V1VERSION,
                SourceDomain = CreateEventConstants.SOURCEDOMAIN,
                SourceSystem = CreateEventConstants.SOURCESYSTEM,
                User = new User
                {
                    // Name = token.Name,
                    // Email = token.Email
                },
                EventData = new EventData
                {
                    NewData = account
                }
            };
        }

        public AccountSns Update(Account account)
        {
            return new AccountSns
            {
                CorrelationId = Guid.NewGuid(),
                DateTime = DateTime.UtcNow,
                EntityId = account.Id,
                Id = Guid.NewGuid(),
                EventType = UpdateEventConstants.EVENTTYPE,
                Version = UpdateEventConstants.V1VERSION,
                SourceDomain = UpdateEventConstants.SOURCEDOMAIN,
                SourceSystem = UpdateEventConstants.SOURCESYSTEM,
                User = new User
                {
                    // Name = token.Name,
                    // Email = token.Email
                },
                EventData = new EventData
                {
                    NewData = account
                }
            };
        }
    }
}
