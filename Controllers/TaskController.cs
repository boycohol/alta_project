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
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepository taskRepository;

        public TaskController(ITaskRepository taskRepository)
        {
            this.taskRepository = taskRepository;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> getTaskById(int id)
        {
            var response = await taskRepository.getTaskByIdAsync(id);
            return StatusCode((int)response.code, response);
        }
        [HttpPost("create")]
        public async Task<IActionResult> createTask(TaskModel taskModel)
        {
            var response = await taskRepository.createTaskAsync(taskModel);
            return StatusCode((int)response.code, response);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> deleteTask(int id)
        {
            var response = await taskRepository.deleteTaskAsync(id);
            return StatusCode((int)response.code, response);
        }
        [HttpGet("not-start-assigned")]
        public async Task<IActionResult> getTaskNotStartorAssigned()
        {
            var response = await taskRepository.getTaskNotAssignedOrStartedAsync();
            return StatusCode((int)response.code, response);
        }
        [HttpGet("user")]
        public async Task<IActionResult> getTaskByUserId(int userId)
        {
            var response = await taskRepository.getTasksByUserIdAsync(userId);
            return StatusCode((int)response.code, response);
        }
        [HttpPost("update/{id}")]
        public async Task<IActionResult> updateTask(int id, TaskModel taskModel)
        {
            var response = await taskRepository.updateTaskAsync(id, taskModel);
            return StatusCode((int)response.code, response);
        }
        [HttpGet("search")]
        public async Task<IActionResult> getTaskByInfo(string info)
        {
            var response = await taskRepository.getTaskByInfoAsync(info);
            return StatusCode((int)response.code, response);
        }
    }
}
