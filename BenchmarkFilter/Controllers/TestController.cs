using BenchmarkFilter.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BenchmarkFilter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {

        [Benchmark]
        [HttpGet("run-service")]
        public async Task<IActionResult> RunService()
        {
            Thread.Sleep(6000);

            return Ok();
        }
    }
}
