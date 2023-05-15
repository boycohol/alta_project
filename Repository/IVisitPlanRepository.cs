using AltaProject.Entity;
using AltaProject.Model.EntityModel;
using AltaProject.Response;

namespace AltaProject.Repository
{
    public interface IVisitPlanRepository
    {
        public Task<ResponseModel> getVisitPlanByUserIdAsync(int userId);
        public Task<ResponseModel> createVisitPlanAsync(int userId, VisitPlanModel planModel);
        public Task<ResponseModel> getVisitPlansNotStartYetAsync();
        public Task<ResponseModel> getVisitPlanByIdAsync(int visitPlanId);
        public Task<ResponseModel> updateVisitPlanAsync(int visitPlanId, VisitPlanModel planModel);
        public Task<ResponseModel> deleteVisitPlanAsync(int visitPlanId);
        public Task<ResponseModel> searchVisitPlanByInfoAsync(string information);
        public Task<ResponseModel> acceptVisitInvitationAsync(int userId, int visitPlanId);
    }
}
