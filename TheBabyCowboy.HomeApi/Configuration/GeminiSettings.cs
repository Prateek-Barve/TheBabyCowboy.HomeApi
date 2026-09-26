namespace TheBabyCowboy.HomeApi.Configuration;

public class GeminiSettings
{
    public string ApiKey { get; set; } = string.Empty;

    public string Model { get; set; } = "gemini-3.8-flash";
}