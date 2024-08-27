namespace Presentation.Contracts.Auth
{
    public class ConfirmEmailRequest
    {
        public string Token { get; set; } = default!;
        public string Email { get; set; } = default!;
    }
}
