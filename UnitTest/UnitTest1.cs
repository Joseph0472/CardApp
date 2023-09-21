using System.Text;
using Newtonsoft.Json;
namespace UnitTest;

public class UnitTest1
{

    private readonly HttpClient _client;

    public UnitTest1()
    {
        // Initialize the HttpClient with the API base address
        _client = new HttpClient { BaseAddress = new Uri("http://localhost:5126") };
    }

    [Fact]
    public async Task Get_All_Cards()
    {
        // Arrange
        var endpoint = "/api/cards";
        // Act
        var response = await _client.GetAsync(endpoint);
        // Assert
        response.EnsureSuccessStatusCode();
    }
    [Fact]
    public async Task Get_One_Card()
    {
        // Arrange
        var cardId = "1234000022340000";
        var endpoint = $"/api/cards/{cardId}";

        // Act
        var response = await _client.GetAsync(endpoint);
        // Assert
        response.EnsureSuccessStatusCode();
    }
    [Fact]
    public async Task Add_One_Card()
    {
        // Arrange
        var payload = new CardModel
        {

        };
        var endpoint = $"/api/cards/add";
        var jsonPayload = JsonConvert.SerializeObject(payload);
        var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PostAsync(endpoint, content);
        // Assert
        response.EnsureSuccessStatusCode();
    }
}
