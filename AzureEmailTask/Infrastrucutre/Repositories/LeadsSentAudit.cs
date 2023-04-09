using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage;
using System.Threading.Tasks;
using Newtonsoft.Json;
using AzureEmailTask.Domain.Contracts.Repositories;
using AzureEmailTask.Model;
using AzureEmailTask.Domain.Entities;


namespace AzureEmailTask.Infrastrucutre.Repository
{
    public class LeadsSentAudit : ILeadsSentAudit
    {

        public async Task<CompleteModelSent> LeadSentAudit(CompleteModel leadDto)
        {
            var sentAuditModel = new CompleteModelSent
            {
                CompleteModel = JsonConvert.SerializeObject(leadDto),
                Successful = true,
                ErrorMessage = string.Empty
            };
            return await Send(sentAuditModel);
        }

        public async Task<CompleteModelSent> LeadSentAuditError(CompleteModel leadDto, string error)
        {
            var sentAuditModel = new CompleteModelSent
            {
                CompleteModel = JsonConvert.SerializeObject(leadDto),
                Successful = false,
                ErrorMessage = error //can error be different?
            };
            return await Send(sentAuditModel);
        }

        private async Task<CompleteModelSent> Send(CompleteModelSent sentAuditModel)
        {
            var storage = CloudStorageAccount.Parse("AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;" +
                "DefaultEndpointsProtocol=http;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;QueueEndpoint=http://127.0.0.1:10001/devstoreaccount1;" +
                "TableEndpoint=http://127.0.0.1:10002/devstoreaccount1;");
            var tableClient = storage.CreateCloudTableClient();
            var table = tableClient.GetTableReference("leadsSentAudit");
            await table.ExecuteAsync(TableOperation.Insert(sentAuditModel));
            return sentAuditModel;
        }
        
    }
}
