using AltaProject.Model.EntityModel;
using AltaProject.Response;

namespace AltaProject.Repository
{
    public interface ICommentRepository
    {
        public Task<ResponseModel> addCommenttoTaskAsync(CommentModel model);
    }
}
