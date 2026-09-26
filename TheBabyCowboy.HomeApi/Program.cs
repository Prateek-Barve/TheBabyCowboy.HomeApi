using TheBabyCowboy.HomeApi.Configuration;
using TheBabyCowboy.HomeApi.Data;
using TheBabyCowboy.HomeApi.Services;


const string ReactCorsPolicy = "ReactCorsPolicy";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddHttpClient<OllamaAIResponseService>(client =>
//{
//    client.BaseAddress = new Uri("http://localhost:11434");
//});


var mongoDbSettings =
    builder.Configuration
        .GetSection("MongoDb")
        .Get<MongoDbSettings>()
    ?? throw new InvalidOperationException(
        "MongoDB configuration is missing."
    );

var geminiSettings =
    builder.Configuration
        .GetSection("Gemini")
        .Get<GeminiSettings>()
    ?? throw new InvalidOperationException(
        "Gemini configuration is missing."
    );

builder.Services.AddSingleton(mongoDbSettings);
builder.Services.AddSingleton(geminiSettings);

builder.Services.AddSingleton<MongoDbContext>();

builder.Services.AddScoped<IThoughtService, ThoughtService>();
builder.Services.AddScoped<IReflectionService, ReflectionService>();
//builder.Services.AddScoped<IAIResponseService>(
//    provider =>
//        provider.GetRequiredService<OllamaAIResponseService>());
builder.Services.AddScoped<IAIResponseService, GeminiAIResponseService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy(ReactCorsPolicy, policy =>
    {
        policy
            .WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(ReactCorsPolicy);

app.UseAuthorization();

app.MapControllers();

app.Run();
