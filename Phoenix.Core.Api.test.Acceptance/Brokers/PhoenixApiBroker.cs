// ------------------------------------------------
// Copyright (c) Coalitions of Inspired Developers
// FREE TO USE FOR THE WORLD
// ------------------------------------------------

using Microsoft.AspNetCore.Mvc.Testing;
using RESTFulSense.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Core.Api.test.Acceptance.Brokers
{
    public partial class PhoenixApiBroker
    {
        private readonly WebApplicationFactory<Startup> webApplicationFactory;
        private readonly HttpClient httpClient;
        private readonly IRESTFulApiFactoryClient apiFactoryClient;

        public PhoenixApiBroker()
        {
            this.webApplicationFactory = new WebApplicationFactory<Startup>();
            this.httpClient = this.webApplicationFactory.CreateClient();
            this.apiFactoryClient = new RESTFulApiFactoryClient(this.httpClient);
        }
    }
}
