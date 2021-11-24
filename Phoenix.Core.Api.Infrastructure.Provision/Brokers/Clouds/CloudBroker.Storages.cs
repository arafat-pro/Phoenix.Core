using System.Threading.Tasks;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;
using Microsoft.Azure.Management.Sql.Fluent;
using Phoenix.Core.Api.Infrastructure.Provision.Models.Storages;

namespace Phoenix.Core.Api.Infrastructure.Provision.Brokers.Clouds
{
    public partial class CloudBroker
    {
        public async ValueTask<ISqlServer> CreateSqlServerASync(
            string sqlServerName,
            IResourceGroup resourceGroup)
        {
            return await this.azure.SqlServers
                .Define(sqlServerName)
                .WithRegion(Region.AsiaEast)
                .WithExistingResourceGroup(resourceGroup)
                .WithAdministratorLogin(this.adminName)
                .WithAdministratorPassword(this.adminAccess)
                .CreateAsync();
        }

        public async ValueTask<ISqlDatabase> CreateSqlDatabase(
            string sqlDatabaseName,
            ISqlServer sqlServer)
        {
            return await this.azure.SqlServers.Databases
                .Define(sqlDatabaseName)
                .WithExistingSqlServer(sqlServer)
                .CreateAsync();
        }

        public SqlDatabaseAccess GetAdminAccess()
        {
            return new SqlDatabaseAccess
            {
                AdminName = this.adminName,
                AdminAccess = this.adminAccess
            };
        }
    }
}