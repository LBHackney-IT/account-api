namespace AccountsApi.V1.Infrastructure.Helpers.Interfaces
{
    public interface IPagingHelper
    {
        int GetPageOffset(int pageSize, int currentPage);
    }
}
