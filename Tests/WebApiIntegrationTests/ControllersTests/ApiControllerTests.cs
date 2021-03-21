using System.Threading.Tasks;
using Backend.WebApi;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace WebApiIntegrationTests.ControllersTests
{
    public class ApiControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public ApiControllerTests()
        {
            _factory = new WebApplicationFactory<Startup>();
        }

        [Fact]
        public async Task Get_All_Server_Async_ReturnSuccessAndGuid()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("Api/Servers");
            response.EnsureSuccessStatusCode();
        }
    }
}