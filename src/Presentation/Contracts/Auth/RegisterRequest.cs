namespace Presentation.Contracts.Auth
{
    public record RegisterRequest(
        string FirstName,
        string LastName,
        string Email,
        string Password,
        string PhoneNumber,
        string Country);
}
