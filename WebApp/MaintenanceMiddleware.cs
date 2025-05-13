using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

public class MaintenanceMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IWebHostEnvironment _env;

    public MaintenanceMiddleware(RequestDelegate next, IWebHostEnvironment env)
    {
        _next = next;
        _env = env;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var maintenanceFilePath = Path.Combine(_env.ContentRootPath, "maintenance.lock");
        if (File.Exists(maintenanceFilePath))
        {
            string message = "The site is under maintenance.";
            try
            {
                var json = await File.ReadAllTextAsync(maintenanceFilePath);
                var data = JsonSerializer.Deserialize<MaintenanceInfo>(json);
                if (!string.IsNullOrEmpty(data?.Message))
                    message = data.Message;
            }
            catch {}

            context.Response.StatusCode = 503;
            context.Response.ContentType = "text/html";
            await context.Response.WriteAsync($"<html><body><h1>{message}</h1></body></html>");
            return;
        }

        await _next(context);
    }
}

public class MaintenanceInfo
{
    public string? Message { get; set; }
}