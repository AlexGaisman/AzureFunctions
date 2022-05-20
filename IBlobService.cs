using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFunctionFirst
{
    public interface IBlobService
    {
        Task<List<string>> GetAllBlobs(string containerName);
        Task<string> GetBlob(string name, string containerName);
        Task<bool> Upload(string name, Stream content, string containerName, 
            string contentType, IDictionary<string, string> methadata = null);

        Task<bool> Delete(string name, string containerName);
    }
}
