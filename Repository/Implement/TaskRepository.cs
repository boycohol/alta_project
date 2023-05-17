using AltaProject.Data;
using AltaProject.Entity;
using AltaProject.Model.EntityModel;
using AltaProject.Response;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Xml;

namespace AltaProject.Repository.Implement
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationDBContext context;
        private readonly IMapper mapper;
        private readonly INotificationRepository notificationRepository;

        public TaskRepository(ApplicationDBContext context, IMapper mapper, INotificationRepository notificationRepository)
        {
            this.context = context;
            this.mapper = mapper;
            this.notificationRepository = notificationRepository;
        }
        public async Task<ResponseModel> createTaskAsync(TaskModel taskModel)
        {
            var task = new VisitTask()
            {
                Title = taskModel.Title,
                Description = taskModel.Description,
                Status = taskModel.Status,
                StartDate = DateTime.Parse(taskModel.StartDate).ToUniversalTime(),
                Category = taskModel.Category,
                Rating = taskModel.Rating,
                CreatorUserId = taskModel.CreatorUserId,
            };
            var user = await context.InternalUsers.FirstOrDefaultAsync(x => x.Id == taskModel.CreatorUserId);
            if (user == null)
            {
                return new ResponseModel(System.Net.HttpStatusCode.NotFound, "Creator Id not found", null);
            }
            if (taskModel.AssigneeStaffId != 0)
            {
                var staff = await context.Staffs.FirstOrDefaultAsync(x => x.Id == taskModel.AssigneeStaffId);
                if (staff == null)
                {
                    return new ResponseModel(System.Net.HttpStatusCode.NotFound, "Assignee Id not found", null);
                }
                task.AssigneeStaff = staff;

                var notify = new NotificationModel()
                {
                    Title = "You're have task" + taskModel.Title,
                    Detail = taskModel.Description,
                    SenderUserId = 0,
                    UserReceiverId = taskModel.AssigneeStaffId
                };
                var response = await notificationRepository.sendNotificationAsync(notify);
            }
            task.CreatorUser = user;
            context.Tasks.Add(task);
            await context.SaveChangesAsync();
            return new ResponseModel(System.Net.HttpStatusCode.OK, "Success", task.Id);
        }

        public async Task<ResponseModel> deleteTaskAsync(int taskId)
        {
            var task = await context.Tasks.FirstOrDefaultAsync(x => x.Id == taskId);
            if (task == null)
            {
                return new ResponseModel(System.Net.HttpStatusCode.NotFound, "Task id not found", null);
            }
            context.Tasks.Remove(task);
            await context.SaveChangesAsync();
            return new ResponseModel(System.Net.HttpStatusCode.OK, "Success", null);
        }

        public async Task<ResponseModel> getTaskNotAssignedOrStartedAsync()
        {
            var dateNow = DateTime.UtcNow;
            var taskModels = await context.Tasks.Where(x => DateTime.Compare(dateNow, x.StartDate) < 0 || x.AssigneeStaff == null)
                .Select(x => mapper.Map<TaskModel>(x)).ToListAsync();
            return new ResponseModel(System.Net.HttpStatusCode.OK, "Success", taskModels);
        }

        public async Task<ResponseModel> getTaskByIdAsync(int taskId)
        {
            var task = await context.Tasks.FirstOrDefaultAsync(x => x.Id == taskId);
            if (task == null)
            {
                return new ResponseModel(System.Net.HttpStatusCode.NotFound, "Task Id not found", null);
            }
            return new ResponseModel(System.Net.HttpStatusCode.OK, "Success", mapper.Map<TaskModel>(task));
            //return file and comments also
        }

        public async Task<ResponseModel> getTasksByUserIdAsync(int userId)
        {
            var taskModels = await context.Tasks.Where(x => x.CreatorUserId == userId || x.AssigneeStaffId == userId)
                .Select(x => mapper.Map<TaskModel>(x)).ToListAsync();
            return new ResponseModel(System.Net.HttpStatusCode.OK, "Success", taskModels);
        }

        public async Task<ResponseModel> updateTaskAsync(int taskId, TaskModel taskModel)
        {
            var task = await context.Tasks.FirstOrDefaultAsync(x => x.Id == taskId);
            if (task == null)
            {
                return new ResponseModel(System.Net.HttpStatusCode.NotFound, "Task Id not found", null);
            }
            task.Status = taskModel.Status;
            task.Title = taskModel.Title;
            task.Description = taskModel.Description;
            task.StartDate = DateTime.Parse(taskModel.StartDate).ToUniversalTime();
            if (taskModel.EndDate != null)
            {
                task.EndDate = DateTime.Parse(taskModel.EndDate).ToUniversalTime();
            }

            task.Category = taskModel.Category;
            if (taskModel.Rating != null)
            {
                task.Rating = taskModel.Rating;
            }
            if (taskModel.AssigneeStaffId != 0)
            {
                if (task.AssigneeStaffId == null || task.AssigneeStaffId != taskModel.AssigneeStaffId)
                {
                    var staff = await context.Staffs.FirstOrDefaultAsync(x => x.Id == taskModel.AssigneeStaffId);
                    if (staff == null)
                    {
                        return new ResponseModel(System.Net.HttpStatusCode.NotFound, "Assignee Id not found", null);
                    }
                    task.AssigneeStaff = staff;
                    task.AssigneeStaffId = taskModel.AssigneeStaffId;
                    var response = await notificationRepository.sendNotificationAsync(new NotificationModel()
                    {
                        Title = "You're have task" + taskModel.Title,
                        Detail = taskModel.Description,
                        SenderUserId = 0,
                        UserReceiverId = taskModel.AssigneeStaffId
                    });
                }
            }
            else
            {
                task.AssigneeStaffId = null;
                task.AssigneeStaff = null;
            }
            await context.SaveChangesAsync();
            return new ResponseModel(System.Net.HttpStatusCode.OK, "Success", null);
        }

        public async Task<ResponseModel> getTaskByInfoAsync(string info)
        {
            var taskModels = await context.Tasks.Where(x => x.Title.Contains(info) || x.Description.Contains(info)
            || x.Status.Contains(info) || x.StartDate.ToString().Contains(info) || x.EndDate.ToString().Contains(info)
            || x.Category.Contains(info) || x.AssigneeStaff.InternalUser.User.Name.Contains(info))
                .Select(x => mapper.Map<TaskModel>(x)).ToListAsync();
            return new ResponseModel(System.Net.HttpStatusCode.OK, "Success", taskModels);
        }
    }
}
