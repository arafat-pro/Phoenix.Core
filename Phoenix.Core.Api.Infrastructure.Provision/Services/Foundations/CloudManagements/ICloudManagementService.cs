using System.Threading.Tasks;
using Microsoft.Azure.Management.ResourceManager.Fluent;

namespace Phoenix.Core.Api.Infrastructure.Provision.Services.Foundations.CloudManagements
{
    public interface ICloudManagementService
    {
        ValueTask<IResourceGroup> ProvisionResourceGroupAsync(
            string projectName,
            string environment);
    }
}
