// ------------------------------------------------
// Copyright (c) Coalitions of Inspired Developers
// FREE TO USE FOR THE WORLD
// ------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Phoenix.Core.Api.test.Acceptance.Brokers
{
    [CollectionDefinition(nameof(ApiTestCollection))]
    public class ApiTestCollection: ICollectionFixture<PhoenixApiBroker>
    {
    }
}
