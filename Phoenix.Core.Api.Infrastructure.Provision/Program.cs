using System.Threading.Tasks;
using Phoenix.Core.Api.Infrastructure.Provision.Services.Processings.CloudManagements;

namespace Phoenix.Core.Api.Infrastructure.Provision
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            ICloudManagementProcessingService cloudManagementProcessingService =
                new CloudManagementProcessingService();

            await cloudManagementProcessingService.ProcessAsync();
        }
    }
}