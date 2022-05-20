using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using MyFunctionFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFunctionFirst
{
    public class TableService: ITableService
    {
        private readonly CloudStorageAccount _cloudStorageAccount;
        public TableService(CloudStorageAccount cloudStorageAccount)
        {
            _cloudStorageAccount = cloudStorageAccount;
        }

        public Task<bool> CreateTable(string name)
        {
            var tableClient = _cloudStorageAccount.CreateCloudTableClient();
            var table = tableClient.GetTableReference(name);
           return table.CreateIfNotExistsAsync();
        }

        public async Task<bool> CreateRecord(string tableName, string game, string user, int score)
        {
            var tableClient = _cloudStorageAccount.CreateCloudTableClient();
            var table = tableClient.GetTableReference(tableName);
            var result = await table.ExecuteAsync(TableOperation.InsertOrReplace(new ScoreEntity(game, user, score)));

            return result != null;
        }

        public async Task<ScoreEntity> GetRecordById(string tableName, string game, string user)
        {
            var tableClient = _cloudStorageAccount.CreateCloudTableClient();
            var table = tableClient.GetTableReference(tableName);
            var result = await table.ExecuteAsync(TableOperation.Retrieve<ScoreEntity>(game, user));

            return (ScoreEntity)result.Result;
        }

        public Task<ScoreEntity> GetScoresGt1000(string tableName)
        {
            //var tableClient = _cloudStorageAccount.CreateCloudTableClient();
            //var table = tableClient.GetTableReference(tableName);
            //table.Que

            return null;
        }
    }
}
