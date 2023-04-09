using Microsoft.WindowsAzure.Storage.Table;
using System;


namespace AzureEmailTask.Domain.Entities
{
    public class CompleteModel : TableEntity
    {
        public string Source { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Date { get; set; }
        public int Size { get; set; }
        public string Service_Type { get; set; }
        public string FromState { get; set; }
        public string FromCity { get; set; }
        public int FromZip { get; set; }
        public string ToState { get; set; }
        public string ToCity { get; set; }
        public int ToZip { get; set; }
        public int MailAttempt { get; set; }

        public CompleteModel()
        {
            PartitionKey = "testPartition";
            RowKey = Guid.NewGuid().ToString();
        }

    }
}
