using AccountsApi.V1.Infrastructure.Helpers.Interfaces;

namespace AccountsApi.V1.Infrastructure.Helpers
{
    public class PagingHelper : IPagingHelper
    {
        public int GetPageOffset(int pageSize, int currentPage)
        {
            return pageSize * (currentPage == 0 ? 0 : currentPage - 1);
        }
    }
}
