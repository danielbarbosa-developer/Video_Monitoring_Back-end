using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Backend.Application.Dtos;
using Backend.Application.Dtos.Input;
using Backend.Application.Handlers;
using Backend.WebApi;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;


namespace WebApiIntegrationTests.ControllersTests
{
    
    public class ApiControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;
        private string ServerId { get; set; }
        private string VideoId { get; set; }

        public ApiControllerTests()
        {
            _factory = new WebApplicationFactory<Startup>();
            SetupTestsEnvironment();
        }

        [Fact]
        public async Task Get_All_Servers_Async_ReturnsSuccessAndServersList()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("Api/Servers");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
        }
        [Fact]
        public async Task Get_Server_By_Id_ReturnSuccessAndServerObject()
        {
            var client = _factory.CreateClient();
            
            var response = await client.GetAsync("Api/servers/" + ServerId);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            Assert.NotEmpty(result);
            
        }

        [Fact]
        public async Task Post_Server_Async_ReturnsSuccessAndGuid()
        {
            var data = new ServerDto()
            {
                Name = "Pier Boulevard",
                IpAddress = "127.000.000",
                Port = 8080
            };
            using var requestMessage = new HttpRequestMessage(HttpMethod.Post, "Api/server");
            var content = await Task.FromResult(JsonSerializer.Serialize(data)).ConfigureAwait(false);
            requestMessage.Content = new StringContent(content, Encoding.UTF8, "application/json");
            var client = _factory.CreateClient();

            var response = await client.SendAsync(requestMessage);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            Assert.NotEmpty(result);
            
        }
        [Fact]
        public async Task Delete_Server_Async()
        {
            var data = new ServerDto()
            {
                Name = "Pier Boulevard",
                IpAddress = "127.000.000",
                Port = 8080
            };
            using var requestMessage = new HttpRequestMessage(HttpMethod.Post, "Api/server");
            var content = await Task.FromResult(JsonSerializer.Serialize(data)).ConfigureAwait(false);
            requestMessage.Content = new StringContent(content, Encoding.UTF8, "application/json");
            var client = _factory.CreateClient();

            var postResponse = await client.SendAsync(requestMessage);
            var postResult = await postResponse.Content.ReadAsStringAsync();
            
           
            var response = await client.DeleteAsync("Api/server/"+postResult);
            response.EnsureSuccessStatusCode();

        }

        [Fact]
        public async Task Post_Video_Async_ReturnsSuccessAndGuid()
        {
            var base64String = VideoFileHandler.ConvertVideoToBase64("../../../../TestsData/KrabVideoTest.mp4");
            var data = new VideoDtoInput()
            {
                Description = "A Krab fishing video", //only for tests
                VideoContent = base64String
            };
            using var requestMessage = new HttpRequestMessage(HttpMethod.Post, "Api/servers/"+ServerId+"/videos");
            var content = await Task.FromResult(JsonSerializer.Serialize(data)).ConfigureAwait(false);
            requestMessage.Content = new StringContent(content, Encoding.UTF8, "application/json");
            var client = _factory.CreateClient();

            var response = await client.SendAsync(requestMessage);
            this.VideoId = await response.Content.ReadAsStringAsync();
            
        }
        private async void SetupTestsEnvironment()
        {
            var data = new ServerDto()
            {
                Name = "Pier Boulevard",
                IpAddress = "127.000.000",
                Port = 8080
            };
            using var requestMessage = new HttpRequestMessage(HttpMethod.Post, "Api/server");
            var content = await Task.FromResult(JsonSerializer.Serialize(data)).ConfigureAwait(false);
            requestMessage.Content = new StringContent(content, Encoding.UTF8, "application/json");
            var client = _factory.CreateClient();

            var response = await client.SendAsync(requestMessage);
            this.ServerId = await response.Content.ReadAsStringAsync();
        }
    }
}