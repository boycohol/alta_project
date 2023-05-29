using AltaProject.Data;
using AltaProject.Entity;
using AltaProject.Model.EntityModel;
using AltaProject.Response;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AltaProject.Repository.Implement
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDBContext context;
        private readonly IMapper mapper;

        public CommentRepository(ApplicationDBContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
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
                CommentDay = DateTime.Parse(model.CommentDay).ToUniversalTime(),
                TaskId = model.TaskId,
                VisitTask = task,
                CommentUser = user,
                CommentUserId = model.CommentUserId
            };
            context.Comments.Add(comment);
            await context.SaveChangesAsync();
            return new ResponseModel(System.Net.HttpStatusCode.OK, "Success", comment.Id);
        }
        public async Task<ResponseModel> getLastestComments()
        {
            var visitTasks = await context.Tasks.OrderByDescending(x => x.EndDate).ToListAsync();
            var comments = new List<Comment>();
            foreach (var task in visitTasks)
            {
                if (task.Comments != null)
                {
                    comments.Concat(task.Comments);
                }
            }
            comments = comments.Take(3).ToList();
            return new ResponseModel(System.Net.HttpStatusCode.OK, "Success", comments);
        }
    }
}
