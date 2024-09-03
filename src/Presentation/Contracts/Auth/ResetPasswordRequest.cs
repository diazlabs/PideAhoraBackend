namespace Presentation.Contracts.Auth
{
    public record ResetPasswordRequest(
        string Email,
        string Password,
        string Token
    );   
}
