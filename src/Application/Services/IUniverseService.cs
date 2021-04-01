using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Application.DTO;

namespace Application.Services
{
    public interface IUniverseService
    {
        Task<UniverseCurrentTimeDto> GetUniverseCurrentTime();
    }

    public class UniverseService : IUniverseService
    {
        private readonly HttpClient _client;

        public UniverseService(HttpClient client)
        {
            _client = client;
        }

        public async Task<UniverseCurrentTimeDto> GetUniverseCurrentTime()
        {
            var response = await _client.GetAsync("universes/time");
            response.EnsureSuccessStatusCode();

            await using var responseStream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<UniverseCurrentTimeDto>(responseStream, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }
    }
}