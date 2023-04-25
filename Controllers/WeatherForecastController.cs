using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using Microsoft.Identity.Web.Resource;
using System.Security.Claims;

namespace AltaProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:scopes")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly GraphServiceClient graphServiceClient;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, GraphServiceClient graphServiceClient)
        {
            _logger = logger;
            this.graphServiceClient = graphServiceClient;
        }
        [Authorize(Roles ="Manager")]
        [HttpGet("WeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
        [HttpPost("token")]
        public async Task<string> getTokenAsync([FromForm]string id_token, [FromForm] string state)
        {
            var token = id_token;
            return token ;
        }
        [HttpPost("access-token")]
        public async Task<string> getAccessToken([FromForm] string access_token, [FromForm] string state)
        {
            var user = await graphServiceClient.Users.Request().GetAsync();
            var token = access_token;
            return token ;
        }
    }
}