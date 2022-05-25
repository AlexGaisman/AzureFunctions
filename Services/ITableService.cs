using MyFunctionFirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFunctionFirst.Services
{
    public interface ITableService
    {
        Task<bool> CreateTable(string name);
        Task<bool> CreateRecord(string tableName, string game, string user, int score);

        Task<ScoreEntity> GetRecordById(string TableName, string game, string user);

        Task<ScoreEntity> GetScoresGt1000(string TableName);

    }
}
