using AltaProject.Model.EntityModel;
using AltaProject.Response;

namespace AltaProject.Repository
{
    public interface IArticleRepository
    {
        public Task<ResponseModel> getArticlesAsync();
        public Task<ResponseModel> updateArticleAsync(int articleId, ArticleModel articleModel);
        public Task<ResponseModel> getArticleByIdAsync(int articleId);
        public Task<ResponseModel> createArticleAsync(ArticleModel articleModel,int userId);
        public Task<ResponseModel> deleteArticleAsync(int articleId);
    }
}
