using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Management.AppService.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;

namespace Phoenix.Core.Api.Infrastructure.Provision.Brokers.Clouds
{
    public partial interface ICloudBroker
    {
        ValueTask<IWebApp> CreateWebAppAsync(
            string webAppName,
            string databaseConnectionString,
            IAppServicePlan plan,
            IResourceGroup resourceGroup);
    }
}
