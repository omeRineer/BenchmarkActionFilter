using BenchmarkFilter.ServiceTools;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace BenchmarkFilter.Filters
{
    public class Benchmark : ActionFilterAttribute, IActionFilter
    {
        readonly int MaxMilliseconds;
        readonly ILogger<Benchmark> logger;
        readonly Stopwatch Stopwatch;

        public Benchmark(int maxTimeout = 5000)
        {
            MaxMilliseconds = maxTimeout;
            logger = StaticServiceProvider.GetService<ILogger<Benchmark>>();
            Stopwatch = Stopwatch.StartNew();
        }
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            Stopwatch.Reset();
            Stopwatch.Start();

            await next();
        }

        public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            Stopwatch.Stop();

            double milliSeconds = Math.Round(Stopwatch.Elapsed.TotalMilliseconds, 2);

            if (milliSeconds > MaxMilliseconds)
                logger.LogWarning($"{context.ActionDescriptor.DisplayName} : Timeout Warning !!");

            await next();

        }

    }
}
