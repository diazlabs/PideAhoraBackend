using Application.Common.Interfaces;
using Ardalis.Result;
using FluentEmail.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Common.Services
{
    public class EmailService : IEmailService
    {
        private readonly IFluentEmail _fluentEmail;
        private readonly ILogger<EmailService> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public EmailService(IFluentEmail fluentEmail, ILogger<EmailService> logger, IHttpContextAccessor httpContextAccessor)
        {
            _fluentEmail = fluentEmail;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        private string GetBaseAddress()
        {
            return $"{_httpContextAccessor.HttpContext!.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}/";
        }

        public async Task<Result> SendConfirmationEmail(string to, string token)
        {
            string confirmationLink = $"{GetBaseAddress}?token={token}";
            var response = await _fluentEmail
                .To(to)
                .Subject("Confirma tu correo electrónico")
                .Body($"Para continuar usando nuestro servicio<a href='{confirmationLink}'>confirma tu correo</a>")
                .SendAsync();

            if (response.Successful)
            {
                return Result.Success();
            }

            _logger.LogError("Error al enviar el confirmation email {to}, {messageId}, {messages}", to, response.MessageId, response.ErrorMessages);

            return Result.Error("Error al enviar el email");
        }

        public async Task<Result> SendResetPasswordEmail(string to, string token)
        {
            string resetLink = $"{GetBaseAddress}?token={token}";
            var response = await _fluentEmail
                .To(to)
                .Subject("Restablecer contraseña")
                .Body($"Para restablecer contraseña<a href='{resetLink}'>haz click aquí</a>")
                .SendAsync();

            if (response.Successful)
            {
                return Result.Success();
            }

            _logger.LogError("Error al enviar el reset password email {to}, {messageId}, {messages}", to, response.MessageId, response.ErrorMessages);

            return Result.Error("Error al enviar el email");
        }
    }
}
