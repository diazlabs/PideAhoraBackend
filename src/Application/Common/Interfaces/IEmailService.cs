using Ardalis.Result;

namespace Application.Common.Interfaces
{
    public interface IEmailService
    {
        Task<Result> SendEmail(string to, string subject, string content);
    }
}
