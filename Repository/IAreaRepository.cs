using AltaProject.Model.EntityModel;
using AltaProject.Response;

namespace AltaProject.Repository
{
    public interface IAreaRepository
    {
        public Task<ResponseModel> getAreasAsync();
        public Task<ResponseModel> getAreaByIdAsync(int id);
        public Task<ResponseModel> createAreaAsync(AreaModel areaModel);
        public Task<ResponseModel> updateAreaAsync(int areaId, AreaModel areaModel);
        public Task<ResponseModel> assignUsersToAreaAsync(int areaId, List<int> userIds);
        public Task<ResponseModel> addUserToAreaAsync(UserModel userModel);
        public Task<ResponseModel> getDistributorsByAreaIdAsync(int areaId);
        public Task<ResponseModel> addDistributorAsync(int areaId, DistributorModel distributorModel);
    }
}
