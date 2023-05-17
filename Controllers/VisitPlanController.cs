using AltaProject.Model.AuthModel;
using AltaProject.Model.EntityModel;
using AltaProject.Repository;
using AltaProject.Repository.Implement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace AltaProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VisitPlanController : ControllerBase
    {
        private readonly IVisitPlanRepository visitPlanRepository;

        public VisitPlanController(IVisitPlanRepository visitPlanRepository)
        {
            this.visitPlanRepository = visitPlanRepository;
        }
        [HttpPost("create")]
        public async Task<IActionResult> createPlan(int userId, VisitPlanModel planModel)
        {
            var response = await visitPlanRepository.createVisitPlanAsync(userId, planModel);
            return StatusCode((int)response.code, response);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> deletePlan(int id)
        {
            var response = await visitPlanRepository.deleteVisitPlanAsync(id);
            return StatusCode((int)response.code, response);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> getVisitPlanById(int id)
        {
            var response = await visitPlanRepository.getVisitPlanByIdAsync(id);
            return StatusCode((int)response.code, response);
        }
        [HttpGet("user")]
        public async Task<IActionResult> getVisitPlanByUserId(int userId)
        {
            var response = await visitPlanRepository.getVisitPlanByUserIdAsync(userId);
            return StatusCode((int)response.code, response);
        }
        [HttpGet("not-start")]
        public async Task<IActionResult> getVisitPlanNotStart()
        {
            var response = await visitPlanRepository.getVisitPlansNotStartYetAsync();
            return StatusCode((int)response.code, response);
        }
        [HttpGet("search")]
        public async Task<IActionResult> getVisitPlanByInfo(string info)
        {
            var response = await visitPlanRepository.searchVisitPlanByInfoAsync(info);
            return StatusCode((int)response.code, response);
        }
        [HttpPost("update/{id}")]
        public async Task<IActionResult> updateVisitPlan(int id, VisitPlanModel planModel)
        {
            var response = await visitPlanRepository.updateVisitPlanAsync(id, planModel);
            return StatusCode((int)response.code, response);
        }
        [HttpPost("accept-plan")]
        public async Task<IActionResult> acceptVisitPlan(int userId, int visitPlanId)
        {
            var response = await visitPlanRepository.acceptVisitInvitationAsync(userId, visitPlanId);
            return StatusCode((int)response.code, response);
        }
    }
}
