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
    public class SurveyController : ControllerBase
    {
        private readonly ISurveyRepository surveyRepository;

        public SurveyController(ISurveyRepository surveyRepository)
        {
            this.surveyRepository = surveyRepository;
        }
        [HttpPost("create")]
        public async Task<IActionResult> createSurvey(SurveyModel surveyModel)
        {
            var response = await surveyRepository.createSurveyAsync(surveyModel);
            return StatusCode((int)response.code, response);
        }
        [HttpGet("asked")]
        public async Task<IActionResult> getAskedSurvey()
        {
            var response = await surveyRepository.getAskedSurveyAsync();
            return StatusCode((int)response.code, response);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> getSurveyById(int id)
        {
            var response = await surveyRepository.getSurveyByIdAsync(id);
            return StatusCode((int)response.code, response);
        }
        [HttpPost("update/{id}")]
        public async Task<IActionResult> updateSurvey(int id, SurveyModel surveyModel)
        {
            var response = await surveyRepository.updateSurveyAsync(id, surveyModel);
            return StatusCode((int)response.code, response);
        }
    }
}
