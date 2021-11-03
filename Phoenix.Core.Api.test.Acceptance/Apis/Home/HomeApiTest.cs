using Phoenix.Core.Api.test.Acceptance.Brokers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Phoenix.Core.Api.test.Acceptance.Apis.Home
{
    [Collection(nameof(ApiTestCollection))]
    public partial class HomeApiTest
    {
        private readonly PhoenixApiBroker phoenixApiBroker;
        public HomeApiTest(PhoenixApiBroker phoenixApiBroker) =>
            this.phoenixApiBroker = phoenixApiBroker;

    }
}
