using Azure.Storage.Blobs;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

            var blob = new BlobServiceClient(configuration.GetValue<string>("blobConnection"));

            builder.Services.AddSingleton<BlobServiceClient>(blob);
            builder.Services.AddSingleton<IContainerService, ContainerService>();
            builder.Services.AddSingleton<IBlobService, BlobService>();

            //   builder.Services.AddSingleton<ILogg>();
        }
    }
}