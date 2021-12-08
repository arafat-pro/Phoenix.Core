using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.Management.AppService.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.Sql.Fluent;
using Phoenix.Core.Api.Infrastructure.Provision.Brokers.Configurations;
using Phoenix.Core.Api.Infrastructure.Provision.Models.Configurations;
using Phoenix.Core.Api.Infrastructure.Provision.Models.Storages;
using Phoenix.Core.Api.Infrastructure.Provision.Services.Foundations.CloudManagements;

namespace Phoenix.Core.Api.Infrastructure.Provision.Services.Processings.CloudManagements
{
    public class CloudManagementProcessingService : ICloudManagementProcessingService
    {
        private readonly ICloudManagementService cloudManagementService;
        private readonly IConfigurationBroker configurationBroker;
        public CloudManagementProcessingService()
        {
            this.cloudManagementService = new CloudManagementService();
            this.configurationBroker = new ConfigurationBroker();
        }

        public async ValueTask ProcessAsync()
        {
            CloudManagementConfiguration cloudManagementConfiguration =
                this.configurationBroker.GetConfigurations();
            await ProvisionAsync(
                projectName: cloudManagementConfiguration.ProjectName,
                cloudAction: cloudManagementConfiguration.Up);
        }

        private async ValueTask ProvisionAsync(
            string projectName,
            CloudAction cloudAction)
        {
            List<string> environments = RetrieveEnvironemnts(cloudAction);
            foreach (string environmentName in environments)
            {
                IResourceGroup resourceGroup =
                    await this.cloudManagementService.ProvisionResourceGroupAsync(
                        projectName,
                        environmentName);

                IAppServicePlan appServicePlan =
                    await this.cloudManagementService.ProvisionPlanAsync(
                        projectName,
                        environmentName,
                        resourceGroup);

                ISqlServer sqlServer =
                    await this.cloudManagementService.ProvisionSqlServerASync(
                        projectName,
                        environmentName,
                        resourceGroup);

                SqlDatabase sqlDatabase =
                    await this.cloudManagementService.ProvisionSqlDatabaseAsync(
                        projectName,
                        environmentName,
                        sqlServer);

                IWebApp webApp =
                    await this.cloudManagementService.ProvisionWebAppAsync(
                        projectName,
                        environmentName,
                        sqlDatabase.ConnectionString,
                        resourceGroup,
                        appServicePlan);
            }

        }

        private static List<string> RetrieveEnvironemnts(CloudAction cloudAction) =>
            cloudAction?.Environments ?? new List<string>();
    }
}