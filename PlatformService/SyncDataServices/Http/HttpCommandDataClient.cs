using PlatformService.Dtos;

namespace PlatformService.SyncDataServices.Http;

public class HttpCommandDataClient : ICommandDataClient
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public HttpCommandDataClient(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }
    public async Task SendPlatformToCommand(PlatformReadDto platform)
    {
        //var httpContent = new StringContent(
        //    JsonSerializer.Serialize(platform),
        //    Encoding.UTF8,
        //    "application/json");

        var response = await _httpClient.PostAsJsonAsync($"{_configuration["CommandService"]}", platform);

        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine("--> sync POST to CommandService was Ok!");
        }
        else
        {
            Console.WriteLine("--> sync POST to CommandService was Not Ok!");
        }
    }
}
