using Azure.Identity;
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
        [Authorize(Roles = "Manager")]
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
        public async Task<string> getTokenAsync([FromForm] string id_token, [FromForm] string state)
        {
            var token = id_token;
            return token;
        }
        [HttpPost("access-token")]
        public async Task<string> getAccessToken([FromForm] string access_token, [FromForm] string state)
        {
            var user = await graphServiceClient.Users.Request().GetAsync();
            var token = access_token;
            return token;
        }
        [HttpGet("user")]
        public async Task<object> getUser(string id)
        {
            var scopes = new[] { "https://graph.microsoft.com/.default" };
            var tenantId = "75777844-ebfa-4db6-b88e-5052a6ed3540";
            var clientId = "f0d264ac-de73-46b8-8329-bca94f13e138";
            var clientSecret = "w798Q~LBzjhl40HsYfimDAOUBxymTbXbHj-rEaz2";

            var options = new TokenCredentialOptions
            {
                AuthorityHost = AzureAuthorityHosts.AzurePublicCloud
            };
            var clientSecretCredential = new ClientSecretCredential(tenantId, clientId, clientSecret, options);
            var graphClient = new GraphServiceClient(clientSecretCredential, scopes);

            var result = await graphClient.Users[id].Request().GetAsync();
            return result;
        }
        [HttpPost("create")]
        public async Task<object> createUser()
        {
            var scopes = new[] { "https://graph.microsoft.com/.default" };
            var tenantId = "75777844-ebfa-4db6-b88e-5052a6ed3540";
            var clientId = "f0d264ac-de73-46b8-8329-bca94f13e138";
            var clientSecret = "w798Q~LBzjhl40HsYfimDAOUBxymTbXbHj-rEaz2";

            var options = new TokenCredentialOptions
            {
                AuthorityHost = AzureAuthorityHosts.AzurePublicCloud
            };
            var clientSecretCredential = new ClientSecretCredential(tenantId, clientId, clientSecret, options);
            var graphClient = new GraphServiceClient(clientSecretCredential, scopes);
            var requestBody = new User
            {
                AccountEnabled = true,
                DisplayName = "Adele Vance",
                MailNickname = "AdeleV",
                PasswordProfile = new PasswordProfile
                {
                    ForceChangePasswordNextSignIn = false,
                    Password = "xWwvJ]6NMw+bWH-d",
                },
            };
            var result = await graphClient.Users.Request().AddAsync(requestBody);
            return result;
        }
        [HttpPost("update-role")]
        public async Task<object> updateUser()
        {
            var scopes = new[] { "AppRoleAssignment.ReadWrite.All" };
            var tenantId = "75777844-ebfa-4db6-b88e-5052a6ed3540";
            var clientId = "f0d264ac-de73-46b8-8329-bca94f13e138";
            var clientSecret = "w798Q~LBzjhl40HsYfimDAOUBxymTbXbHj-rEaz2";
            var authorizationCode = "0.ASsARHh3dfrrtk24jlBSpu01QKxk0vBz3rhGgym8qU8T4TgrAC8.AgABAAIAAAD--DLA3VO7QrddgJg7WevrAgDs_wUA9P_hKkhWBUkBdEmaHHw90rWKfeDfw2c1r5XqO9iG0ucFRSCmJmLs9eNkYfnIMnCbRWOwZxex-UDrZ30l0iSJMZPv4GDqJaU4R-mXyFQ7rGaV0kIyInWIeapkrfJMZ5yB27sDaEEfYJdCFjiQumMfbKxNS_JT3KndEAjIOtQ3avXM-5FJJsf04vIK4onX32aSCnyu4nKTApUQJkBqnY7Wh9yS3R6JQ_20zH7y_S5HCREocJbO2S4IStmzjf85IgPJmwKKORdfhLpdsa2uvoxy5IX1DiGqxzHQAJ90x_z2HfQHs4TFxSctMOa7AE3O6J_k3Gz5eD0_Qh4wYiqn6lGUbWpZCNRcewvYIsJFbs1vP_MNkY0bpNQ5C68TMVheUpEpkSBB6L8UOUbmQPHo8VGwQMH-FvY1pgUk4UqLaUIi0k_N18P1c83eI4nRfwrfsXIIZpIfSB8sPDcoiEAkkn3ywYEQN4uDggSUMMXijLi1Eb-9SGDBNeGtmiFqp_0AHAQvZPJ1SfeVmaNbr0x_vvj8f8j2kFkyX6-pwCoOoIobterL3Uvlu_w0yhpy-2Ogn_yJkZbD9i-bX2J6yd3fSnCHtbD7t6sA599q2AmWY77UGfgP_K-RJo_hTUbY5sAY5T3Ct8xXMxLssMYk25kKnBEv7umQ2jgeg0qscO0Gfs0CUMbvNkKV4lviG5l707rOvXZPhOdKEyQJvZs4BCBhaqAuSeN_TEDR7qdMQPJH5m7LzWr8GiZqVfrdwR2sKFNL6JRbsuNacpO2-EteRXhMtzEGy3ZjPfcshIkUjHpRBtXmcoh7matuqHO2GIMVjR3Cf2WDaEKHFJBpeiJkj5s6t61-QYuXTO4OLTkkCxk5M788hwGbG1hPE2oQXzclt2rTaJZIz2YNyD96yCAcgno2zzLEdGD8mQvdBIc3XfUt39fMqStTYmu4hW9d9_uxhxoH7O3ARTM3dqCl68W8e7PD5jET14lNpI4tc7hgBhS2Q9n2bNVfSFl6Wcq0APFw3Iqx-hqZDdrkVyCqBpzq849SCwaf87t6sMH5bnJrq1PBCSKBE6EohUiQPIrX953y6LYcRPxcMRmfwn7o7HG9dMovipadPNBhu-APoIJTnjaukzBd3GzyY4EQDW1sUzJntT37OJ0DbszIWLSDPrikm6863U3lNC1k6HGneaHMldUcL5g9vnwBPYA4mEQDUOLNtSM-tJYIYy57XQ45-Xag0SrpR8j1nKBu-I36rP4sEs3rUmH53fAUudVeBlB393zJZh3zTQUlb1wGWYg";
            var options = new TokenCredentialOptions
            {
                AuthorityHost = AzureAuthorityHosts.AzurePublicCloud
            };
            var authCodeCredential = new AuthorizationCodeCredential(tenantId, clientId, clientSecret, authorizationCode, options);
            var graphClient = new GraphServiceClient(authCodeCredential, scopes);
            var requestBody = new AppRoleAssignment
            {
                PrincipalId = Guid.Parse("40a2307b-2d04-4b8b-9653-74f3ad556ef8"),
                ResourceId = Guid.Parse("6bd0f1eb-d3de-404d-b804-0ec74a43a66c"),
                AppRoleId = Guid.Parse("00f1b2db-52c7-4faf-9b1d-370905a826d8"),
            };
            var result = await graphClient.Users["40a2307b-2d04-4b8b-9653-74f3ad556ef8"].AppRoleAssignments.Request().AddAsync(requestBody);
            return result;
        }
    }
}