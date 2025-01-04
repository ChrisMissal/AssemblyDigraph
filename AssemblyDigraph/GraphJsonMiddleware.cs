using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace AssemblyDigraph;

public class GraphJsonMiddleware
{
    private readonly RequestDelegate _next;
    private readonly Assembly _assembly;
    private readonly string? _pathString;

    public GraphJsonMiddleware(RequestDelegate next, Assembly assembly, string? pathString = "/graph")
    {
        _next = next;
        _assembly = assembly;
        _pathString = pathString;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path.StartsWithSegments(_pathString))
        {
            var graph = new[] { _assembly }.ToDigraph();

            var jsonGraph = graph.ToJson(type => type.FullName ?? type.Name);
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(jsonGraph, Encoding.UTF8);
        }
        else
        {
            await _next(context);
        }
    }
}