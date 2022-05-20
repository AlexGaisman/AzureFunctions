using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFunctionFirst.Models
{
    public class ScoreEntity: TableEntity
    {
        public ScoreEntity() { }
        public ScoreEntity(string gameName, string userName, int topScore)
        {
            PartitionKey = gameName;
            RowKey = userName;
            TopScore = topScore;
        }

        public string Game => PartitionKey;
        public string UserName => RowKey;
        public int TopScore { get; set; }

        public override string ToString() => $"{Game} {UserName} {TopScore}";

    }
}
