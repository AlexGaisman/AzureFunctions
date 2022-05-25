using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFunctionFirst.Services
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

            await foreach (BlobContainerItem item in _blobClient.GetBlobContainersAsync())
            {
                containerName.Add(item.Name);
            }

            return containerName;
        }

        public async Task<List<string>> GetAllContainersAndBlobs()
        {
            List<string> containerAndBlobNames = new();
            containerAndBlobNames.Add("Account Name :" + _blobClient.AccountName);
            containerAndBlobNames.Add("---------------------------");
            await foreach (BlobContainerItem blobContainerItem in _blobClient.GetBlobContainersAsync())
            {
                containerAndBlobNames.Add("-- " + blobContainerItem.Name);
                BlobContainerClient blobContainer = _blobClient.GetBlobContainerClient(blobContainerItem.Name);
                await foreach (BlobItem blobItem in blobContainer.GetBlobsAsync())
                {
                    // get methadata
                    var blobClient = blobContainer.GetBlobClient(blobItem.Name);
                    BlobProperties blobProperties = await blobClient.GetPropertiesAsync();

                    foreach (var pair in blobProperties.Metadata)
                    {
                        containerAndBlobNames.Add($"--- Property {pair.Key}={pair.Value}");
                    }


                    containerAndBlobNames.Add("--- " + blobItem.Name);
                }

                containerAndBlobNames.Add("---------------------------------");
            }
            return containerAndBlobNames;
        }
    }
}
