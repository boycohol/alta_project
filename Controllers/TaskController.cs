using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace AltaProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:scopes")]
    public class TaskController : ControllerBase
    {

    }
}
