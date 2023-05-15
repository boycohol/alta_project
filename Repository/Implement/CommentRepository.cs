using AltaProject.Data;
using AltaProject.Entity;
using AltaProject.Model.EntityModel;
using AltaProject.Response;
using Microsoft.EntityFrameworkCore;

namespace AltaProject.Repository.Implement
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDBContext context;

        public CommentRepository(ApplicationDBContext context)
        {
            this.context = context;
        }
        public async Task<ResponseModel> addCommenttoTaskAsync(CommentModel model)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Id == model.CommentUserId);
            if (user == null)
            {
                return new ResponseModel(System.Net.HttpStatusCode.NotFound, "User Id not found", null);
            }
            var task = await context.Tasks.FirstOrDefaultAsync(x => x.Id == model.TaskId);
            if (task == null)
            {
                return new ResponseModel(System.Net.HttpStatusCode.NotFound, "Task Id not found", null);
            }
            var comment = new Comment()
            {
                CommentText = model.CommentText,
                TaskId = model.TaskId,
                VisitTask = task,
                CommentUser = user,
                CommentUserId = model.CommentUserId
            };
            context.Comments.Add(comment);
            await context.SaveChangesAsync();
            return new ResponseModel(System.Net.HttpStatusCode.OK, "Success", comment.Id);
        }
    }
}
