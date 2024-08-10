using Application.Common.Interfaces;
using Ardalis.Result;

namespace Infrastructure.Common.Services
{
    public class EmailService : IEmailService
    {
        public async Task<Result> SendEmail(string to, string subject, string content)
        {
            return Result.Success();
        }
    }
}
