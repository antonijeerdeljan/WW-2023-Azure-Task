using System;
using Microsoft.WindowsAzure.Storage.Table;

namespace AzureEmailTask.Model
{
    public class CompleteModelSent : TableEntity
    {
        public string CompleteModel { get; set; }
        public bool Successful { get; set; }
        public string ErrorMessage { get; set; }

        public CompleteModelSent()
        {
            PartitionKey = "testPartition";
            RowKey = Guid.NewGuid().ToString();
        }

    }
}
