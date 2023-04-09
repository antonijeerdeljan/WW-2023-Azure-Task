using AzureEmailTask.Domain.Entities;
using AzureEmailTask.Model;
using System.Threading.Tasks;

namespace AzureEmailTask.Domain.Contracts.Repositories
{
    public interface ILeadsSentAudit
    {
        Task<CompleteModelSent> LeadSentAudit(CompleteModel leadDto);
        Task<CompleteModelSent> LeadSentAuditError(CompleteModel leadDto, string error);
    }
}