namespace Presentation.Contracts.Auth
{
    public record ResetPasswordRequest(
        Guid UserId,
        string Password,
        string Token
    );   
}
