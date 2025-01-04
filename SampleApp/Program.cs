using System.Reflection;
using AssemblyDigraph;

namespace SampleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseMiddleware<GraphJsonMiddleware>(Assembly.GetExecutingAssembly());
            // Serve the D3.js visualization HTML file
            app.MapGet("/", async context =>
            {
                var htmlPath = Path.Combine(Directory.GetCurrentDirectory(), "graph.html");
                context.Response.ContentType = "text/html";
                await context.Response.SendFileAsync(htmlPath);
            });

            app.Run();
        }
    }
}
