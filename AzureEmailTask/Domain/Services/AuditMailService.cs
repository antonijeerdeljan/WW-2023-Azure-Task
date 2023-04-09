using AzureEmailTask.Domain.Contracts.Email;
using AzureEmailTask.Domain.DTO;
using AzureEmailTask.Infrastrucutre.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AzureEmailTask.Domain.Services
{
    public class AuditMailService
    {
        private readonly IEmailService _emailService;

        public AuditMailService(IEmailService emailService)
        {
            _emailService = emailService;
        }
        public async Task<IActionResult> SendEmails(CompleteModelDto completeModelDto)
        {

            LeadRepo leadRepo = new LeadRepo();
            var leads = await leadRepo.GetAllLeads();

            var emailsSent = 0;
            var leadsSentAudit = new LeadsSentAudit();

            foreach (var lead in leads)
            {
                var resultResponse = (ObjectResult)_emailService.SendEmail(CreateMail(completeModelDto), completeModelDto.Date, lead.Email);
                if (resultResponse is OkObjectResult)
                {
                    await leadsSentAudit.LeadSentAudit(lead);
                    await leadRepo.DeleteLead(lead);

                    emailsSent++;
                }
                else
                {
                    if (lead.MailAttempt == 5)
                    {
                        await leadsSentAudit.LeadSentAuditError(lead, resultResponse.Value.ToString());
                        await leadRepo.DeleteLead(lead);

                        return new BadRequestObjectResult(resultResponse.Value.ToString());
                    }

                    lead.MailAttempt++;
                    await leadRepo.IncrementMailSender(lead);
                    //return new BadRequestObjectResult(resultResponse.Value);
                }
            }

            return new OkObjectResult($"Emails sent: {emailsSent}");
        }

        private static string CreateMail(CompleteModelDto completeModelDto)
        {
            var messageTemplate = $"Dear sales,\r\n{completeModelDto.Name.First} {completeModelDto.Name.Last} has reqestest a move with following data.\r\n" +
                                  $"Date: {completeModelDto.Date}\r\nSize: {completeModelDto.Size}\r\nService type: {completeModelDto.Service_Type}\r\n" +
                                  $"Moving from: {completeModelDto.From.City}, {completeModelDto.From.Zip}, {completeModelDto.From.State}\r\n" +
                                  $"Moving to: {completeModelDto.To.City}, {completeModelDto.To.Zip}, {completeModelDto.To.State}\r\n" +
                                  $"You can contact the lead by email {completeModelDto.Contact.Email}\r\nOr by phone {completeModelDto.Contact.Phone}\r\n" +
                                  $"Kind regards,\r\nLead collector";
            return messageTemplate;
        }
    }
}
