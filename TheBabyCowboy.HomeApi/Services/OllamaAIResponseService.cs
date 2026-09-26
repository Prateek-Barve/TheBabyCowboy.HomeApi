using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace TheBabyCowboy.HomeApi.Services;

public class OllamaAIResponseService : IAIResponseService
{
    private readonly HttpClient _httpClient;

    private const string ModelName = "qwen3:4b";

    public OllamaAIResponseService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> GenerateResponseAsync(
        string thought,
        string visitorResponse,
        CancellationToken cancellationToken = default)
    {
        var prompt = $"""
            You are a thoughtful companion on a personal website.

            A visitor was shown this thought:

            "{thought}"

            The visitor responded:

            "{visitorResponse}"

            Respond to their reflection thoughtfully.

            Rules:
            - Keep the response between 2 and 4 sentences.
            - Be warm, human, and reflective.
            - Do not ask a follow-up question.
            - Do not give generic motivational advice.
            - Do not mention that you are an AI.
            - Respond specifically to what the visitor wrote.
            """;

        var request = new OllamaChatRequest
        {
            Model = ModelName,
            Stream = false,
            Messages =
            [
                new OllamaMessage
                {
                    Role = "user",
                    Content = prompt
                }
            ]
        };

        var response = await _httpClient.PostAsJsonAsync(
            "/api/chat",
            request,
            cancellationToken);

        response.EnsureSuccessStatusCode();

        var result =
            await response.Content.ReadFromJsonAsync<OllamaChatResponse>(
                cancellationToken);

        if (result?.Message?.Content is null ||
            string.IsNullOrWhiteSpace(result.Message.Content))
        {
            throw new InvalidOperationException(
                "Ollama returned an empty response.");
        }

        return result.Message.Content.Trim();
    }

    private class OllamaChatRequest
    {
        public string Model { get; set; } = string.Empty;

        public bool Stream { get; set; }

        public List<OllamaMessage> Messages { get; set; } = [];
    }

    private class OllamaMessage
    {
        public string Role { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;
    }

    private class OllamaChatResponse
    {
        public OllamaMessage? Message { get; set; }
    }
}