using System.Threading.Tasks;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;

namespace Phoenix.Core.Api.Infrastructure.Provision.Brokers.Clouds
{
    public partial class CloudBroker
    {
        public async ValueTask<bool> CheckResourceGroupExistAsync(string resourceGroupName) =>
            await this.azure.ResourceGroups.ContainAsync(resourceGroupName);

        public async ValueTask<IResourceGroup> CreateResourceGroupAsync(string resourceGroupName)
        {
            return await this.azure.ResourceGroups
                .Define(name: resourceGroupName)
                .WithRegion(region: Region.AsiaEast)
                .CreateAsync();
        }

        public async ValueTask DeleteResourceGroupAsync(string resourceFroupName) =>
            await this.azure.ResourceGroups.DeleteByNameAsync(resourceFroupName);
    }
}