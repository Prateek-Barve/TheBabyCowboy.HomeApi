using Google.GenAI;
using Google.GenAI.Types;
using TheBabyCowboy.HomeApi.Configuration;

namespace TheBabyCowboy.HomeApi.Services;

public class GeminiAIResponseService : IAIResponseService
{
    private readonly GeminiSettings _settings;
    private readonly Client _client;

    public GeminiAIResponseService(
        GeminiSettings settings)
    {
        _settings = settings;

        if (string.IsNullOrWhiteSpace(_settings.ApiKey))
        {
            throw new InvalidOperationException(
                "Gemini API key is missing."
            );
        }

        _client = new Client(
            apiKey: _settings.ApiKey
        );
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

            Respond thoughtfully to their reflection.

            Rules:
            - Keep the response in less that 11 words.
            - Be warm, human, and reflective.
            - Respond specifically to what the visitor wrote.
            - Do not ask a follow-up question.
            - Do not give generic motivational advice.
            - Do not mention that you are an AI.
            - Do not use bullet points.
            - Do not use quotation marks around your response.
            """;

        var response =
            await _client.Models.GenerateContentAsync(
                model: _settings.Model,
                contents: prompt,
                cancellationToken: cancellationToken
            );

        var text =
            response.Candidates?
                .FirstOrDefault()?
                .Content?
                .Parts?
                .FirstOrDefault()?
                .Text;

        if (string.IsNullOrWhiteSpace(text))
        {
            throw new InvalidOperationException(
                "Gemini returned an empty response."
            );
        }

        return text.Trim();
    }
}