using AzureEmailTask.Domain.Contracts.Email;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AzureEmailTask.Infrastrucutre.Services
{
    public class EmailService : IEmailService
    {
        public IActionResult SendEmail(string body, string date, string email)
        {
            try
            {
                string fromMail = "movingcompanydemo@gmail.com";
                string fromPassword = "buzstyssqhqrvoon";

                MailMessage message = new MailMessage();
                message.From = new MailAddress(fromMail);
                message.Subject = $"A New Lead Has Arrived for {date}";
                message.To.Add(new MailAddress(email));
                message.Body = body;

                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(fromMail, fromPassword),
                    EnableSsl = true
                };

                smtpClient.Send(message);

                return new OkObjectResult(true);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex);
            }
        }
    }
}
