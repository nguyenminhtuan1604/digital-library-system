using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Override ReverseProxy configuration dynamically from environment variables
var catalogUrl = Environment.GetEnvironmentVariable("CATALOG_SERVICE_URL") ?? "http://catalog-service:5001";
var circulationUrl = Environment.GetEnvironmentVariable("CIRCULATION_SERVICE_URL") ?? "http://circulation-service:5002";
var identityUrl = Environment.GetEnvironmentVariable("IDENTITY_SERVICE_URL") ?? "http://identity-service:5003";

builder.Configuration["ReverseProxy:Clusters:catalog-cluster:Destinations:destination1:Address"] = catalogUrl;
builder.Configuration["ReverseProxy:Clusters:circulation-cluster:Destinations:destination1:Address"] = circulationUrl;
builder.Configuration["ReverseProxy:Clusters:identity-cluster:Destinations:destination1:Address"] = identityUrl;

// Add YARP
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors("AllowAll");

// Health check endpoint
app.MapGet("/health", () => Results.Ok(new { status = "Healthy", service = "API Gateway" }));

// Map reverse proxy
app.MapReverseProxy();

app.Run();
