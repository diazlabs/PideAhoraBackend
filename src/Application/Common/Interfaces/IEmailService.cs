using Ardalis.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IEmailService
    {
        Task<Result> SendEmail(string to, string subject, string content);
    }
}
