using System.Net;
using System.Text.Json;
using System.Web;

namespace omdb_backend;

public class OMDbMovieRepository(string baseUrl, string apiKey)
{
    private readonly HttpClient _httpClient = new();

    public async Task<ShortMovie[]?> GetAllByTitle(string title, int page = 1)
    {
        var url = $"{baseUrl}?apiKey={apiKey}&s={HttpUtility.UrlEncode(title)}&page={page}";
        var response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(response.ReasonPhrase, null, response.StatusCode);
        }

        var content = await response.Content.ReadAsStreamAsync();
        var json = await JsonSerializer.DeserializeAsync<MovieSearchResult>(content);

        return json?.Search ?? [];
    }

    public async Task<FullMovie?> GetById(string id)
    {
        var url = $"{baseUrl}?apiKey={apiKey}&i={HttpUtility.UrlEncode(id)}";
        var response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException(response.ReasonPhrase, null, response.StatusCode);
        }

        var content = await response.Content.ReadAsStreamAsync();

        try
        {
            return await JsonSerializer.DeserializeAsync<FullMovie>(content);
        }
        catch (JsonException exception)
        {
            content.Position = 0;
            var error = await JsonSerializer.DeserializeAsync<ErrorResponse>(content);
            throw new HttpRequestException(error?.Error, exception, HttpStatusCode.BadRequest);
        }
    }
}

internal record MovieSearchResult
{
    public ShortMovie[]? Search { get; init; }
}

public record ErrorResponse
{
    public required string Response { get; init; }
    public required string Error { get; init; }
}