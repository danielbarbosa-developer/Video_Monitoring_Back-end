using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Backend.Application.Dtos;
using Backend.Application.Dtos.Input;
using Backend.Application.Handlers;
using Backend.Domain.Entities;
using Backend.WebApi;
using Microsoft.AspNetCore.Mvc.Testing;
using WebApiIntegrationTests.Setup;
using Xunit;



namespace WebApiIntegrationTests.ControllersTests
{
    
    public class ApiControllerTests : BaseControllerTest, IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;
        public ApiControllerTests()
        {
            _factory = new WebApplicationFactory<Startup>();
        }

        [Fact]
        public async Task Get_All_Servers_Async_ReturnsSuccessAndServersList()
        {
            string serverId = await SetupServerToTest(_factory);
            var client = _factory.CreateClient();
            var response = await client.GetAsync("Api/servers");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            Assert.NotEmpty(result);
            await ClearServerData(_factory, serverId);
            
        }
        [Fact]
        public async Task Get_Server_By_Id_ReturnSuccessAndServerObject()
        {
            string serverId = await SetupServerToTest(_factory);
            var client = _factory.CreateClient();
            
            var response = await client.GetAsync("Api/servers/" + serverId);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            Assert.NotEmpty(result);
            await ClearServerData(_factory, serverId);

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
            await ClearServerData(_factory, result);
            
        }
        [Fact]
        public async Task Delete_Server_Async()
        {
            string serverId = await SetupServerToTest(_factory);
            var client = _factory.CreateClient();

            
            var response = await client.DeleteAsync($"Api/server/{serverId}");
            response.EnsureSuccessStatusCode();
        }
        [Fact]
        public async Task Post_Video_Async_ReturnsSuccessAndGuid()
        {
            string serverId = await SetupServerToTest(_factory);
            var base64String = VideoFileHandler.ConvertVideoToBase64("../../../../TestsData/KrabVideoTest.mp4");
            var data = new VideoDtoInput()
            {
                Description = "A Krab fishing video", //only for tests
                VideoContent = base64String
            };
            using var requestMessage = new HttpRequestMessage(HttpMethod.Post, $"Api/servers/{serverId}/videos");
            var content = await Task.FromResult(JsonSerializer.Serialize(data)).ConfigureAwait(false);
            requestMessage.Content = new StringContent(content, Encoding.UTF8, "application/json");
            var client = _factory.CreateClient();

            var response = await client.SendAsync(requestMessage);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            Assert.NotEmpty(result);
            Assert.NotNull(result);
            await ClearVideoData(_factory, result);
            await ClearServerData(_factory, serverId);

        }

        [Fact]
        public async Task Get_Video_Information_Async_ReturnSuccessAndVideoInformationDto()
        {
            string serverId = await SetupServerToTest(_factory);
            string videoId = await SetupVideoToTest(_factory, serverId);
            var client = _factory.CreateClient();
            var response = await client.GetAsync($"Api/servers/{serverId}/videos/{videoId}");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            Assert.NotEmpty(result);
            Assert.NotNull(result);
            await ClearVideoData(_factory, videoId);
            await ClearServerData(_factory, serverId);
        }
        [Fact]
        public async Task Get_All_Videos_Information_Async_ReturnSuccessAndVideoInformationDtoList()
        {
            var serverId = await SetupServerToTest(_factory);
            var videoId = await SetupVideoToTest(_factory, serverId);
            var client = _factory.CreateClient();
            var response = await client.GetAsync($"Api/servers/{serverId}/videos");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            Assert.NotEmpty(result);
            Assert.NotNull(result);
            await ClearVideoData(_factory, videoId);
            await ClearServerData(_factory, serverId);
        }
        [Fact]
        public async Task Download_Video_From_Server_Async_ReturnSuccessAndBase64()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("Api/servers/00facf75-4984-4f11-80a6-e8a491d1b489/videos/aeebe28b-317f-45be-a072-8c3d89f095c7/binary");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            Assert.NotEmpty(result);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Delete_Video_From_Server_Async_ReturnSuccess()
        {
            var serverId = await SetupServerToTest(_factory);
            var videoToDelete = await SetupVideoToTest(_factory, serverId);
            var client = _factory.CreateClient();
            
            var response = await client.DeleteAsync($"Api/servers/{serverId}/videos/{videoToDelete}");
            response.EnsureSuccessStatusCode();
            await ClearServerData(_factory, serverId);
        }

        [Fact]
        public async Task Recycle_All_Videos_Async_ReturnStatusOk()
        {
            var client = _factory.CreateClient();
            using var requestMessage = new HttpRequestMessage(HttpMethod.Post, "Api/recycler/process/22-03-2021");
            var response = await client.SendAsync(requestMessage);
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Get_Recycle_Operation_Status_Async_ReturnSuccessAndStatus()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("Api/recycle/status");
            response.EnsureSuccessStatusCode();
        }
    }
}