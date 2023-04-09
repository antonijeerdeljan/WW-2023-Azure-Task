using AzureEmailTask.Domain.Entities;
using Microsoft.WindowsAzure.Storage.Table;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AzureEmailTask.Domain.Contracts.Repositories
{
    public interface ILeadRepo
    {
        Task<CompleteModel> DeleteLead(CompleteModel completeModel);
        Task<List<CompleteModel>> GetAllLeads();
        CloudTable GetTable();
        Task<CompleteModel> IncrementMailSender(CompleteModel completeModel);
    }
}