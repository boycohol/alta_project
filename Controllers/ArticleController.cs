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
    public class ArticleController : ControllerBase
    {
        private readonly IArticleRepository articleRepository;

        public ArticleController(IArticleRepository articleRepository)
        {
            this.articleRepository = articleRepository;
        }
        [HttpGet]
        public async Task<IActionResult> getAllArticles()
        {
            var response = await articleRepository.getArticlesAsync();
            return StatusCode((int)response.code, response);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> getArticleById(int id)
        {
            var response = await articleRepository.getArticleByIdAsync(id);
            return StatusCode((int)response.code, response);
        }
        [HttpPost("create")]
        public async Task<IActionResult> createArticle(ArticleModel articleModel, int userId)
        {
            var response = await articleRepository.createArticleAsync(articleModel, userId);
            return StatusCode((int)response.code, response);
        }
        [HttpPost("update/{id}")]
        public async Task<IActionResult> updateArticle(int id, ArticleModel articleModel)
        {
            var response = await articleRepository.updateArticleAsync(id, articleModel);
            return StatusCode((int)response.code, response);
        }
    }
}
