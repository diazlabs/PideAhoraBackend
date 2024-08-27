using Ardalis.Result;

namespace Application.Common.Interfaces
{
    public interface IEmailService
    {
        Task<Result> SendConfirmationEmail(string to, string token);
        Task<Result> SendResetPasswordEmail(string to, string token);
    }
}
