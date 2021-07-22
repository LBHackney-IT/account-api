using Microsoft.EntityFrameworkCore;

namespace AccountsApi.V1.Infrastructure
{

    public class AccountContext : DbContext
    {
        //TODO: rename DatabaseContext to reflect the data source it is representing. eg. MosaicContext.
        //Guidance on the context class can be found here https://github.com/LBHackney-IT/lbh-accounts-api/wiki/DatabaseContext
        public AccountContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<AccountDbEntity> AccountEntities { get; set; }
    }
}
