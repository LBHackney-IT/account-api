using AccountsApi.V1.Domain;
using Microsoft.EntityFrameworkCore;

namespace AccountsApi.V1.Infrastructure
{

    public class AccountContext : DbContext
    {
        public AccountContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<AccountDbEntity> AccountEntities { get; set; }
    }
}
