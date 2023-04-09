using AzureEmailTask.Infrastrucutre.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Threading.Tasks;
using CompleteModelDto = AzureEmailTask.Domain.DTO.CompleteModelDto;
using CompleteModelDtoValidator = AzureEmailTask.Domain.DTO.CompleteModelDtoValidator;
using JsonSerializer = System.Text.Json.JsonSerializer;
using LeadRepo = AzureEmailTask.Infrastrucutre.Repository.LeadRepo;
using AzureEmailTask.FunctionApp.Helpers;
using AzureEmailTask.Domain.Services;
using AzureEmailTask.Domain.Contracts.Email;

namespace AzureEmailTask.FunctionApp
{
    public  class SendEmail
    {
        private readonly AuditMailService _auditMailService;
        //private readonly IEmailService _emailService;
        public SendEmail(AuditMailService auditMailService)
        {
            _auditMailService = auditMailService;
        }

        [FunctionName("SendEmail")]
        public  async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            
            var requestBody = await req.ReadAsStringAsync();
            try
            {
                var completeModelDto = JsonSerializer.Deserialize<CompleteModelDto>(requestBody, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    PropertyNameCaseInsensitive = true
                });
                var validationResult = RequestHelper.IsValidRequest(new CompleteModelDtoValidator(), completeModelDto);

                if (validationResult is BadRequestObjectResult)
                    return validationResult;


                var result = await _auditMailService.SendEmails(completeModelDto);

                return new OkObjectResult(result);

            }
            catch (JsonException)
            {
                return new BadRequestObjectResult("Invalid request body format.");
            }

            /*var validationResult = RequestHelper.IsValidRequest(new CompleteModelDtoValidator(), completeModelDto);

            if (validationResult is BadRequestObjectResult)
                return validationResult;


            var result = await SendEmails(completeModelDto);

            return new OkObjectResult(result);*/
        }

        

    }
}
