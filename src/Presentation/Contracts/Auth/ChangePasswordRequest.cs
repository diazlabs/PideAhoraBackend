namespace Presentation.Contracts.Auth
{
    public record ChangePasswordRequest(
        string OldPassword,
        string NewPassword,
        string ConfirmPassword,
        Guid UserId
    );
}
