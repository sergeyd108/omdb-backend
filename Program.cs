using omdb_backend;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);
builder.Services.AddCors();
var app = builder.Build();

app.UseCors(policy => policy
    .AllowAnyMethod()
    .AllowAnyOrigin()
    .AllowAnyHeader());
app.UseMiddleware<HttpErrorHandlerMiddleware>();

var baseUrl = app.Configuration["App:BaseUrl"];
var apiKey = app.Configuration["App:ApiKey"];

if (baseUrl == null || apiKey == null)
{
    throw new Exception("Required settings are missing.");
}

var repository = new OMDbMovieRepository(baseUrl, apiKey);

app.MapGet("/search/{title}/{page?}", async (string title, string? page) =>
{
    if (int.TryParse(page, out int pageNum))
    { 
        return await repository.GetAllByTitle(title, pageNum);
    }
    
    return await repository.GetAllByTitle(title);
});

app.MapGet("/movie/{id}", async (string id) => await repository.GetById(id));

app.Run();