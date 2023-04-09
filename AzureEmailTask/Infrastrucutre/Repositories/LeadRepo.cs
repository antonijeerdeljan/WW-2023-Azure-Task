using System.Collections.Generic;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System.Threading.Tasks;
using CompleteModel = AzureEmailTask.Domain.Entities.CompleteModel;
using AzureEmailTask.Domain.Contracts.Repositories;

namespace AzureEmailTask.Infrastrucutre.Repository
{
    public class LeadRepo : ILeadRepo
    {

        public CloudTable GetTable()
        {
            var storage = CloudStorageAccount.Parse("AccountName=devstoreaccount1;AccountKey=Eby8vdM02xNOcqFlqUwJPLlmEtlCDXJ1OUzFT50uSRZ6IFsuFq2UVErCz4I6tq/K1SZFPTOtr/KBHBeksoGMGw==;" +
                "DefaultEndpointsProtocol=http;BlobEndpoint=http://127.0.0.1:10000/devstoreaccount1;QueueEndpoint=http://127.0.0.1:10001/devstoreaccount1;" +
                "TableEndpoint=http://127.0.0.1:10002/devstoreaccount1;");
            var tableClient = storage.CreateCloudTableClient();
            var table = tableClient.GetTableReference("testTable");
            return table;
        }

        public async Task<CompleteModel> DeleteLead(CompleteModel completeModel)
        {
            var table = GetTable();
            await table.ExecuteAsync(TableOperation.Delete(completeModel));
            return completeModel;
        }

        public async Task<CompleteModel> IncrementMailSender(CompleteModel completeModel)
        {
            var table = GetTable();
            await table.ExecuteAsync(TableOperation.Replace(completeModel));
            return completeModel;
        }

        public async Task<List<CompleteModel>> GetAllLeads()
        {
            var table = GetTable();
            var query = new TableQuery<CompleteModel>();
            TableContinuationToken continuationToken = null;
            var result = new List<CompleteModel>();
            do
            {
                var queryResult = await table.ExecuteQuerySegmentedAsync(query, continuationToken);
                continuationToken = queryResult.ContinuationToken;
                result.AddRange(queryResult.Results);
            }
            while (continuationToken != null);
            return result;
        }



    }
}
