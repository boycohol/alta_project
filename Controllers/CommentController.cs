using AltaProject.Model.EntityModel;
using AltaProject.Repository;
using AltaProject.Repository.Implement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace AltaProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:scopes")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository commentRepository;

        public CommentController(ICommentRepository commentRepository)
        {
            this.commentRepository = commentRepository;
        }
        [HttpPost]
        public async Task<IActionResult> addCommenttoTask(CommentModel commentModel)
        {
            var response = await commentRepository.addCommenttoTaskAsync(commentModel);
            return StatusCode((int)response.code, response);
        }
    }
}
