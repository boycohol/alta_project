using AltaProject.Data;
using AltaProject.Entity;
using AltaProject.Model.EntityModel;
using AltaProject.Response;
using Microsoft.EntityFrameworkCore;

namespace AltaProject.Repository.Implement
{
    public class SurveyRepository : ISurveyRepository
    {
        private readonly ApplicationDBContext context;

        public SurveyRepository(ApplicationDBContext context)
        {
            this.context = context;
        }
        public async Task<ResponseModel> createSurveyAsync(SurveyModel surveyModel)
        {
            var creatorUser = await context.InternalUsers.FirstOrDefaultAsync(x => x.Id == surveyModel.CreatorUserId);
            if (creatorUser == null)
            {
                return new ResponseModel(System.Net.HttpStatusCode.NotFound, "Creator User Id not found", null);
            }
            var questionnaire = await context.Questionnaire.FirstOrDefaultAsync(x => x.Id == surveyModel.QuestionnaireId);
            if (questionnaire == null)
            {
                return new ResponseModel(System.Net.HttpStatusCode.NotFound, "Questionnaire Id not found", null);
            }
            var implementUsers = new List<User>();
            foreach (var id in surveyModel.ImplementUserIds)
            {
                var user = await context.Users.FirstOrDefaultAsync(x => x.Id == id);
                if (user != null)
                {
                    implementUsers.Add(user);
                }
            }
            var survey = new Survey()
            {
                Title = surveyModel.Title,
                StartDate = DateTime.Parse(surveyModel.StartDate).ToUniversalTime(),
                EndDate = DateTime.Parse(surveyModel.StartDate).ToUniversalTime(),
                CreatorUserId = surveyModel.CreatorUserId,
                CreatorUser = creatorUser,
                ImplementUsers = implementUsers,
                Questionnaire = questionnaire,
                QuestionnaireId = surveyModel.QuestionnaireId
            };
            context.Surveys.Add(survey);
            await context.SaveChangesAsync();
            return new ResponseModel(System.Net.HttpStatusCode.OK, "Success", survey.Id);
        }

        public async Task<ResponseModel> getAskedSurveyAsync()
        {
            var dateNow = DateTime.UtcNow;
            var surveys = await context.Surveys.Where(x => x.EndDate < dateNow || x.EndDate == dateNow).ToListAsync();
            var surveyModels = new List<SurveyModel>();
            foreach (var s in surveys)
            {
                var implementUserIds = new List<int>();
                foreach (var user in s.ImplementUsers)
                {
                    implementUserIds.Add(user.Id);
                }
                surveyModels.Add(new SurveyModel()
                {
                    Title = s.Title,
                    StartDate = s.StartDate.ToShortDateString(),
                    EndDate = s.EndDate.ToShortDateString(),
                    CreatorUserId = s.CreatorUserId,
                    QuestionnaireId = s.QuestionnaireId,
                    ImplementUserIds = implementUserIds
                });
            }
            return new ResponseModel(System.Net.HttpStatusCode.OK, "Success", surveyModels);
        }

        public async Task<ResponseModel> getSurveyByIdAsync(int surveyId)
        {
            var survey = await context.Surveys.FirstOrDefaultAsync(x => x.Id == surveyId);
            if (survey == null)
            {
                return new ResponseModel(System.Net.HttpStatusCode.NotFound, "Survey Id not found", null);
            }
            var implementUserIds = new List<int>();
            foreach (var user in survey.ImplementUsers)
            {
                implementUserIds.Add(user.Id);
            }
            var surveyModel = new SurveyModel()
            {
                Title = survey.Title,
                StartDate = survey.StartDate.ToShortDateString(),
                EndDate = survey.EndDate.ToShortDateString(),
                CreatorUserId = survey.CreatorUserId,
                QuestionnaireId = survey.QuestionnaireId,
                ImplementUserIds = implementUserIds
            };
            return new ResponseModel(System.Net.HttpStatusCode.OK, "Success", surveyModel);
        }

        public async Task<ResponseModel> updateSurveyAsync(int surveyId, SurveyModel surveyModel)
        {
            var survey = await context.Surveys.FirstOrDefaultAsync(x => x.Id == surveyId);
            if (survey == null)
            {
                return new ResponseModel(System.Net.HttpStatusCode.NotFound, "Survey Id not found", null);
            }
            var questionnaire = await context.Questionnaire.FirstOrDefaultAsync(x => x.Id == surveyModel.QuestionnaireId);
            if (questionnaire == null)
            {
                return new ResponseModel(System.Net.HttpStatusCode.NotFound, "Questionnaire Id not found", null);
            }
            var implementUserIds = new List<int>();
            foreach (var user in survey.ImplementUsers)
            {
                implementUserIds.Add(user.Id);
            }
            //Check old and new Implement_User
            var model_implementUserIds = surveyModel.ImplementUserIds;
            var oldUserIds = implementUserIds.Except(model_implementUserIds).ToList();
            var newUserIds = model_implementUserIds.Except(implementUserIds).ToList();
            //Update
            survey.Title = surveyModel.Title;
            survey.StartDate = DateTime.Parse(surveyModel.StartDate).ToUniversalTime();
            survey.EndDate = DateTime.Parse(surveyModel.EndDate).ToUniversalTime();
            survey.Questionnaire = questionnaire;
            survey.QuestionnaireId = surveyModel.QuestionnaireId;
            //Remove old users and Add new users
            foreach (var id in oldUserIds)
            {
                var removeUser = survey.ImplementUsers.FirstOrDefault(x => x.Id == id);
                if (removeUser == null)
                {
                    return new ResponseModel(System.Net.HttpStatusCode.InternalServerError, "Something went wrong", null);
                }
                survey.ImplementUsers.Remove(removeUser);
            }
            foreach (var id in newUserIds)
            {
                var addUser = await context.Users.FirstOrDefaultAsync(x => x.Id == id);
                if (addUser == null)
                {
                    return new ResponseModel(System.Net.HttpStatusCode.InternalServerError, "Something went wrong", null);
                }
                survey.ImplementUsers.Add(addUser);
            }
            await context.SaveChangesAsync();
            return new ResponseModel(System.Net.HttpStatusCode.OK, "Success", null);
        }
        //CommentRepository
    }
}
