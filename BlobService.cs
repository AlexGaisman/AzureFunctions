using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFunctionFirst
{
    internal class BlobService : IBlobService
    {

        private readonly BlobServiceClient _blobClient;
        public BlobService(BlobServiceClient blobClient)
        {
            _blobClient = blobClient;
        }
        public async Task<bool> Delete(string name, string containerName)
        {
            BlobContainerClient blobContainerCLient = _blobClient.GetBlobContainerClient(containerName);
            var blobClient = blobContainerCLient.GetBlobClient(name);

            return await blobClient.DeleteIfExistsAsync();
        }

        public async Task<List<string>> GetAllBlobs(string containerName)
        {
            BlobContainerClient client = _blobClient.GetBlobContainerClient(containerName);
            var blobs = client.GetBlobsAsync();

            var blobNames = new List<string>();

            await foreach(var blob in blobs)
            {
                blobNames.Add(blob.Name);
            }

            return blobNames;
        }

        public async Task<string> GetBlob(string name, string containerName)
        {
            BlobContainerClient blobContainerCLient = _blobClient.GetBlobContainerClient(containerName);
            var blobClient = blobContainerCLient.GetBlobClient(name);

            return blobClient.Uri.AbsoluteUri;
        }

        public async Task<bool> Upload(string name, Stream content, 
                                 string containerName, string contentType, 
                                 IDictionary<string,string> methadata = null)
        {
            BlobContainerClient blobContainerCLient = _blobClient.GetBlobContainerClient(containerName);
            var blobClient = blobContainerCLient.GetBlobClient(name);

            var httpHeaders = new BlobHttpHeaders()
            {
                ContentType = contentType
            };

            Azure.Response<BlobContentInfo> result = null;

            if(methadata == null)
                 result = await blobClient.UploadAsync(content, httpHeaders);
            else 
                result = await blobClient.UploadAsync(content, httpHeaders, methadata);

            return result != null;
        }
    }
}
