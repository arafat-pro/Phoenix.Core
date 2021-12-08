using System.Threading.Tasks;
using Microsoft.Azure.Management.ResourceManager.Fluent;

namespace Phoenix.Core.Api.Infrastructure.Provision.Brokers.Clouds
{
    public partial interface ICloudBroker
    {
        ValueTask<IResourceGroup> CreateResourceGroupAsync(string resourceGroupName);
        ValueTask DeleteResourceGroupAsync(string resourceFroupName);
        ValueTask<bool> CheckResourceGroupExistAsync(string resourceGroupName);
    }
}