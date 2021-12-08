using Microsoft.Azure.Management.Sql.Fluent;

namespace Phoenix.Core.Api.Infrastructure.Provision.Models.Storages
{
    public class SqlDatabase
    {
        public string ConnectionString { get; set; }
        public ISqlDatabase Database { get; set; }
    }
}