namespace Application.Common.Security
{
    public interface ICurrentUserProvider
    {
        CurrentUser GetCurrentUser();
        Guid GetUserId();
    }
}
