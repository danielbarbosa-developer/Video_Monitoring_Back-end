using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Backend.Application.Dtos;
using Backend.Application.Dtos.Input;
using Backend.Application.Handlers;
using Backend.WebApi;
using Microsoft.AspNetCore.Mvc.Testing;

namespace WebApiIntegrationTests.Setup
{
    public class BaseControllerTest
    {
        protected async Task<string> SetupServerToTest( WebApplicationFactory<Startup> factory)
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
            var client = factory.CreateClient();

            var response = await client.SendAsync(requestMessage);
            return await response.Content.ReadAsStringAsync();
        }

        protected async Task<string> SetupVideoToTest(WebApplicationFactory<Startup> factory, string serverId)
        {
            var base64String = VideoFileHandler.ConvertVideoToBase64("../../../../TestsData/KrabVideoTest.mp4");
            var data = new VideoDtoInput()
            {
                Description = "A Krab fishing video", //only for tests
                VideoContent = base64String
            };
            using var requestMessage = new HttpRequestMessage(HttpMethod.Post, "Api/servers/"+serverId+"/videos");
            var content = await Task.FromResult(JsonSerializer.Serialize(data)).ConfigureAwait(false);
            requestMessage.Content = new StringContent(content, Encoding.UTF8, "application/json");
            var client = factory.CreateClient();

            var response = await client.SendAsync(requestMessage);
            return await response.Content.ReadAsStringAsync();
        }

        protected async Task ClearVideoData(WebApplicationFactory<Startup> factory, string videoToDelete)
        {
            var client = factory.CreateClient();
            await client.DeleteAsync($"Api/servers/00facf75-4984-4f11-80a6-e8a491d1b489/videos/{videoToDelete}");
        }

        protected async Task ClearServerData(WebApplicationFactory<Startup> factory, string serverToDelete)
        {
            var client = factory.CreateClient();
            await client.DeleteAsync($"Api/servers/{serverToDelete}");
        }
    }
}