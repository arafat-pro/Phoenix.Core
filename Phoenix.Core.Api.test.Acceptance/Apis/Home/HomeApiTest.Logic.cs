using FluentAssertions;
using Phoenix.Core.Api.test.Acceptance.Brokers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Phoenix.Core.Api.test.Acceptance.Apis.Home
{
    public partial class HomeApiTest
    {
        [Fact]
        public async Task ShouldReturnHomeMessageAsync() 
        {
            //given
            string expectedHomeMessage =
                "Hello Adam, Eve will tempt you to the forbidden tree!";

            //when
            string actualHomeMessage =
                await this.phoenixApiBroker.GetHomeMessageAsync();

            //then
            actualHomeMessage.Should().BeEquivalentTo(expectedHomeMessage);
        }
    }
}
