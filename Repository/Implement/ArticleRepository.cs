using AltaProject.Data;
using AltaProject.Entity;
using AltaProject.Model.EntityModel;
using AltaProject.Response;
using Microsoft.EntityFrameworkCore;

namespace AltaProject.Repository.Implement
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly ApplicationDBContext context;

        public ArticleRepository(ApplicationDBContext context)
        {
            this.context = context;
        }
        public async Task<ResponseModel> createArticleAsync(ArticleModel articleModel, int userId)
        {
            var user = await context.InternalUsers.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
            {
                return new ResponseModel(System.Net.HttpStatusCode.NotFound, "User id not found", null);
            }
            var file = await context.File.FirstOrDefaultAsync(x => x.Id == articleModel.FileId);
            if (file == null)
            {
                return new ResponseModel(System.Net.HttpStatusCode.NotFound, "File id not found", null);
            }
            var article = new Article()
            {
                Title = articleModel.Title,
                HyperText = articleModel.HyperText,
                Description = articleModel.Description,
                Status = articleModel.Status,
                CreatorUser = user,
                File = file,
                CreatedDate = DateTime.UtcNow
            };
            context.Articles.Add(article);
            await context.SaveChangesAsync();
            return new ResponseModel(System.Net.HttpStatusCode.OK, "Success", article.Id);
        }

        public async Task<ResponseModel> deleteArticleAsync(int articleId)
        {
            var article = await context.Articles.FirstOrDefaultAsync(x => x.Id == articleId);
            if (article == null)
            {
                return new ResponseModel(System.Net.HttpStatusCode.NotFound, "Article id not found", null);
            }
            context.Articles.Remove(article);
            await context.SaveChangesAsync();
            return new ResponseModel(System.Net.HttpStatusCode.OK, "Success", null);
        }

        public async Task<ResponseModel> getArticleByIdAsync(int articleId)
        {
            var article = await context.Articles.FirstOrDefaultAsync(x => x.Id == articleId);
            if (article == null)
            {
                return new ResponseModel(System.Net.HttpStatusCode.NotFound, "Article id not found", null);
            }
            return new ResponseModel(System.Net.HttpStatusCode.OK, "Success", new ArticleModel()
            {
                Title = article.Title,
                HyperText = article.HyperText,
                Description = article.Description,
                Status = article.Status,
                CreatorUserId = article.CreatorUser.Id,
                FileId = article.File.Id,
                CreatedDate = article.CreatedDate.ToShortDateString()
            });
        }

        public async Task<ResponseModel> getArticlesAsync()
        {
            var articleModels = await context.Articles.Select(x => new ArticleModel()
            {
                Title = x.Title,
                HyperText = x.HyperText,
                Description = x.Description,
                Status = x.Status,
                CreatorUserId = x.CreatorUser.Id,
                FileId = x.File.Id,
                CreatedDate = x.CreatedDate.ToShortDateString()
            }).ToListAsync();
            return new ResponseModel(System.Net.HttpStatusCode.OK, "Success", articleModels);
        }

        public async Task<ResponseModel> updateArticleAsync(int articleId, ArticleModel articleModel)
        {
            var article = await context.Articles.FirstOrDefaultAsync(x => x.Id == articleId);

            if (article == null)
            {
                return new ResponseModel(System.Net.HttpStatusCode.NotFound, "Article id not found", null);
            }
            var creatorUser = article.CreatorUser;
            if (creatorUser.Id != articleModel.CreatorUserId && articleModel.CreatorUserId != 0)
            {
                creatorUser = await context.InternalUsers.FirstOrDefaultAsync(x => x.Id == articleModel.CreatorUserId);
            }
            var file = article.File;
            if ((file != null && file.Id != articleModel.FileId && articleModel.FileId != 0) || file == null)
            {
                file = await context.File.FirstOrDefaultAsync(x => x.Id == articleModel.FileId);
            }
            article.Title = articleModel.Title;
            article.Description = articleModel.Description;
            article.HyperText = articleModel.HyperText;
            article.Status = articleModel.Status;
            article.CreatorUser = creatorUser;
            article.File = file;
            return new ResponseModel(System.Net.HttpStatusCode.OK, "Success", article.Id);
        }
    }
}
