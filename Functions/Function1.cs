using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using MyFunctionFirst.Services;

namespace MyFunctionFirst.Functions
{
    public class Function1
    {
        private readonly IConfiguration _configuration;
        private readonly IContainerService _containerService;
        private readonly IBlobService _blobService;
        private readonly ITableService _tableService;

        public Function1(IConfiguration configuration,
            IContainerService containerService,
            IBlobService blobService,
            ITableService tableService)
        {
            _configuration = configuration;
            _containerService = containerService;
            _blobService = blobService;
            _tableService = tableService;
        }

        [FunctionName("Function1")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;


            var digest = await _containerService.GetAllContainersAndBlobs();

            var names = await _containerService.GetAllContainers();

            //  await _containerService.CreateContainer("new");
            // var blobCLient = new BlobServiceClient(_configuration.GetValue<string>("BlobConnection"));

            var blobs = await _blobService.GetAllBlobs("images");

            FileStream stream = new FileStream("IMG_1886.JPG", FileMode.Open, FileAccess.Read);

            await _blobService.Upload("IMG_1886.JPG", stream, "images", "image/jpeg");

            await _blobService.Delete("IIMG_1886.JPG", "images");



            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";

            return new OkObjectResult(responseMessage);
        }

        [FunctionName("TableTest")]
        public async Task<IActionResult> TableTest(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var result = await _tableService.CreateTable("mytable");

            result = await _tableService.CreateRecord("mytable", "Snake", "Andrea", 1000);
            result = await _tableService.CreateRecord("mytable", "Snake", "Stefano", 2000);

            var record = await _tableService.GetRecordById("mytable", "Snake", "Stefano");

            string responseMessage =
                "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response.";

            return new OkObjectResult(responseMessage);
        }
    }
}
