using System;
using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace MyFunctionFirst.Functions
{
    public class Function
    {
        //[FunctionName("Function")]
        //public void Run([BlobTrigger("samples-workitems/{name}", Connection = "MyAzureStorageConnectionString")]Stream myBlob, string name, ILogger log)
        //{
        //    log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {myBlob.Length} Bytes");
        //}
    }
}
