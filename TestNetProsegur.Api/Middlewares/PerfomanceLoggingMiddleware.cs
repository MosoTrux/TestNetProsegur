using Serilog;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace TestNetProsegur.Api.Middlewares
{
    public class PerfomanceLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<PerfomanceLoggingMiddleware> _logger;
        private static readonly ConcurrentDictionary<string, (long totalTime, int requestCount)> _routeStats = new();

        public PerfomanceLoggingMiddleware(RequestDelegate next, ILogger<PerfomanceLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();

            await _next(context);

            stopwatch.Stop();

            var route = context.Request.Path.Value;

            _routeStats.AddOrUpdate(route!, (stopwatch.ElapsedMilliseconds, 1), (_, data) =>
            {
                var (totalTime, requestCount) = data;
                return (totalTime + stopwatch.ElapsedMilliseconds, requestCount + 1);
            });


            if (context.Response.StatusCode == 200)
            {
                var (averageTime, requestCount) = _routeStats[route!];

                var logData = $"Route: {route}, Time: {stopwatch.ElapsedMilliseconds}ms Average Time: {averageTime / requestCount}ms, Total Requests: {requestCount}";
                Log.Information(logData);
            }

        }
    }
}
