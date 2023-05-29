using AltaProject.Model.EntityModel;
using AltaProject.Response;

namespace AltaProject.Repository
{
    public interface ISurveyRepository
    {
        public Task<ResponseModel> createSurveyAsync(SurveyModel surveyModel);
        public Task<ResponseModel> getAskedSurveyAsync();
        public Task<ResponseModel> getSurveyByIdAsync(int surveyId);
        public Task<ResponseModel> updateSurveyAsync(int surveyId, SurveyModel surveyModel);
    }
}
