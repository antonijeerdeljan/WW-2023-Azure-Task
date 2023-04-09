using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureEmailTask.Domain.Contracts.Email
{
    public interface IEmailService
    {
        public IActionResult SendEmail(string body, string date, string email);
    }
}
