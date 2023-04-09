using AzureEmailTask.Domain.Contracts.Email;
using AzureEmailTask.Domain.Services;
using AzureEmailTask.Infrastrucutre.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: FunctionsStartup(typeof(AzureEmailTask.FunctionApp.Startup))]
namespace AzureEmailTask.FunctionApp
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddHttpClient();


            builder.Services.AddScoped<IEmailService>((s) =>
            {
                return new EmailService();
            });

            builder.Services.AddScoped<AuditMailService>();
        }
    }
}
