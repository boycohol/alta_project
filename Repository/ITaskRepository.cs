using AltaProject.Model.EntityModel;
using AltaProject.Response;

namespace AltaProject.Repository
{
    public interface ITaskRepository
    {
        public Task<ResponseModel> getTasksByUserIdAsync(int userId);
        public Task<ResponseModel> getTaskByIdAsync(int taskId);
        public Task<ResponseModel> createTaskAsync(TaskModel taskModel);
        public Task<ResponseModel> getTaskNotAssignedOrStartedAsync();
        public Task<ResponseModel> updateTaskAsync(int taskId, TaskModel taskModel);
        public Task<ResponseModel> deleteTaskAsync(int taskId);
        public Task<ResponseModel> getTaskByInfoAsync(string info);
    }
}
