using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFunctionFirst
{
    internal class ContainerService : IContainerService
    {
        private readonly BlobServiceClient _blobClient;

        public ContainerService(BlobServiceClient blobClient)
        {
            _blobClient = blobClient;
        }

        public async Task CreateContainer(string containerName)
        {
            BlobContainerClient client = _blobClient.GetBlobContainerClient(containerName);
            await client.CreateIfNotExistsAsync(PublicAccessType.BlobContainer);
        }

        public async Task DeleteContainer(string containerName)
        {
            BlobContainerClient client = _blobClient.GetBlobContainerClient(containerName);
            await client.DeleteIfExistsAsync();
        }

        public async Task<List<string>> GetAllContainers()
        {
            List<string> containerName = new();

            await foreach(BlobContainerItem item in _blobClient.GetBlobContainersAsync())
            {
                containerName.Add(item.Name);
            }

            return containerName;
        }

        public Task<List<string>> GetAllContainersAndBlobs()
        {
            throw new NotImplementedException();
        }
    }
}
