using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Core.Api.test.Acceptance.Brokers
{
    public partial class PhoenixApiBroker
    {
        private const string HomeRelativeUrl = "api/home";
        public async ValueTask<string> GetHomeMessageAsync() =>
            await this.apiFactoryClient.GetContentStringAsync(HomeRelativeUrl);
    }
}
