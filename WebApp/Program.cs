using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseMiddleware<MaintenanceMiddleware>();

app.MapGet("/", () => "Hello, World! App is live!");

app.Run();