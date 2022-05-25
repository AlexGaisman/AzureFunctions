using Azure.Storage.Blobs;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.WindowsAzure.Storage;
using MyFunctionFirst.Services;

[assembly: FunctionsStartup(typeof(MyFunctionFirst.Startup))]

namespace MyFunctionFirst
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var configuration = builder.GetContext().Configuration;
            //  builder.Services.AddHttpClient();

            //builder.Services.AddSingleton<IMyService>((s) => {
            //    return new MyService();
            //});

            var blob = new BlobServiceClient(configuration.GetValue<string>("BlobConnection"));
            var storageAccountName = configuration.GetValue<string>("AzureStorageAccountName");
            var storageAccountKey = configuration.GetValue<string>("AzureStorageAccountKey");
            var storageCredentials = new Microsoft.WindowsAzure.Storage.Auth.StorageCredentials(storageAccountName, storageAccountKey);
            var cloudStorageAccount = new CloudStorageAccount(storageCredentials, true);

            builder.Services.AddSingleton<BlobServiceClient>(blob);
            builder.Services.AddSingleton<IContainerService, ContainerService>();
            builder.Services.AddSingleton<IBlobService, BlobService>();
            builder.Services.AddSingleton<CloudStorageAccount>(cloudStorageAccount);
            builder.Services.AddSingleton<ITableService, TableService>();

            //   builder.Services.AddSingleton<ILogg>();
        }
    }
}