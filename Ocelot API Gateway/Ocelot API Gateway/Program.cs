using Ocelot.DependencyInjection;
using Ocelot.Middleware;


var builder = WebApplication.CreateBuilder(args);

// Configure Environment
builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddJsonFile("ocelot.json")
    .AddEnvironmentVariables();

// Configure Ocelot
builder.Services.AddOcelot();




// Configure Logger
builder.Services.AddLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
});

// Configure Routing
builder.Services.AddRouting();


// Configure App
var app = builder.Build();

app.UseRouting();
app.MapGet("/", async context =>
{
    await context.Response.WriteAsync("Hello world!");
});



app.Run();

