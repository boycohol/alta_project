using AltaProject.Model.EntityModel;
using AltaProject.Response;

namespace AltaProject.Repository.Implement
{
    public class SurveyRepository : ISurveyRepository
    {
        public Task<ResponseModel> createSurveyAsync(SurveyModel surveyModel)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel> getAskedSurveyAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel> getSurveyByIdAsync(int surveyId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel> updateSurveyAsync(int surveyId)
        {
            throw new NotImplementedException();
        }
    }
}
