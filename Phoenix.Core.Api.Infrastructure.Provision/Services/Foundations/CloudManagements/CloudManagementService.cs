﻿using System.Threading.Tasks;
using Microsoft.Azure.Management.AppService.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.Sql.Fluent;
using Phoenix.Core.Api.Infrastructure.Provision.Brokers.Clouds;
using Phoenix.Core.Api.Infrastructure.Provision.Brokers.Loggings;
using Phoenix.Core.Api.Infrastructure.Provision.Models.Storages;

namespace Phoenix.Core.Api.Infrastructure.Provision.Services.Foundations.CloudManagements
{
    public class CloudManagementService : ICloudManagementService
    {
        private readonly ICloudBroker cloudBroker;
        private readonly ILoggingBroker loggingBroker;

        public CloudManagementService(ICloudBroker cloudBroker, ILoggingBroker loggingBroker)
        {
            this.cloudBroker = new CloudBroker();
            this.loggingBroker = new LoggingBroker();
        }

        public async ValueTask<IResourceGroup> ProvisionResourceGroupAsync(
            string projectName,
            string environment)
        {
            string resourceGroupName = $"{projectName}-RESOURCES-{environment}".ToUpper();
            this.loggingBroker.LogActivity(message: $"Provisioning {resourceGroupName}...");

            IResourceGroup resourceGroup = await this.cloudBroker.CreateResourceGroupAsync(resourceGroupName);

            this.loggingBroker.LogActivity(message: $"{resourceGroupName} Provisioned.");

            return resourceGroup;
        }

        public async ValueTask<IAppServicePlan> ProvisionPlanAsync(
            string projectName,
            string environment,
            IResourceGroup resourceGroup)
        {
            string planName = $"{projectName}-PLAN-{environment}".ToUpper();
            this.loggingBroker.LogActivity(message: $"{planName} Provisioning...");
            IAppServicePlan plan = await this.cloudBroker.CreatePlanAsync(planName, resourceGroup);
            this.loggingBroker.LogActivity(message: $"{plan} Provisioned.");

            return plan;
        }

        public async ValueTask<ISqlServer> ProvisionSqlServerASync(
            string projectName,
            string environment,
            IResourceGroup resourceGroup)
        {
            string sqlServerName = $"{projectName}-dbserver-{environment}".ToLower();
            this.loggingBroker.LogActivity(message: $"Provisioning {sqlServerName}...");

            ISqlServer sqlServer =
                await this.cloudBroker.CreateSqlServerAsync(
                    sqlServerName,
                    resourceGroup);

            this.loggingBroker.LogActivity(message: $"{sqlServer} Provisioned.");

            return sqlServer;
        }

        public async ValueTask<SqlDatabase> ProvisionSqlDatabaseAsync(
            string projectname,
            string environment,
            ISqlServer sqlServer)
        {
            string sqlDatabaseName = $"{projectname}-db-{environment}".ToLower();
            this.loggingBroker.LogActivity(message: $"Provisioning {sqlDatabaseName}...");

            ISqlDatabase sqlDatabase = await this.cloudBroker.CreateSqlDatabaseAsync(
                sqlDatabaseName,
                sqlServer);

            this.loggingBroker.LogActivity(message: $"{sqlDatabaseName} Provisioned.");

            return new SqlDatabase
            {
                Database = sqlDatabase,
                ConnectionString = GenerateConnectionString(sqlDatabase)
            };
        }

        public async ValueTask<IWebApp> ProvisionWebAppAsync(
            string projectName,
            string environment,
            string databaseConnectionString,
            IResourceGroup resourceGroup,
            IAppServicePlan appServicePlan)
        {
            string webAppName = $"{projectName}-{environment}".ToLower();
            this.loggingBroker.LogActivity(message: $"Provisoning {webAppName}...");

            IWebApp webApp = await this.cloudBroker.CreateWebAppAsync(
                    webAppName,
                    databaseConnectionString,
                    appServicePlan,
                    resourceGroup);

            this.loggingBroker.LogActivity(message: $"{webAppName} Provisioned.");

            return webApp;
        }

        private string GenerateConnectionString(ISqlDatabase sqlDatabase)
        {
            SqlDatabaseAccess sqlDatabaseAccess =
                this.cloudBroker.GetAdminAccess();

            return $"Server = tcp:{sqlDatabase.SqlServerName}.database.windows.net,1433;" +
                    $"Initial Catalog={sqlDatabase.Name};" +
                    $"User ID = {sqlDatabaseAccess.AdminName}; " +
                    $"Password = {sqlDatabaseAccess.AdminAccess}; ";
        }
    }
}