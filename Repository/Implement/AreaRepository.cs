using AltaProject.Data;
using AltaProject.Entity;
using AltaProject.Model.EntityModel;
using AltaProject.Response;
using Microsoft.EntityFrameworkCore;

namespace AltaProject.Repository.Implement
{
    public class AreaRepository : IAreaRepository
    {
        private readonly ApplicationDBContext context;

        public AreaRepository(ApplicationDBContext context)
        {
            this.context = context;
        }
        public async Task<ResponseModel> addDistributorAsync(int areaId, DistributorModel distributorModel)
        {
            var area = await context.Area.FirstOrDefaultAsync(x => x.Id == areaId);
            if (area == null)
            {
                return new ResponseModel(System.Net.HttpStatusCode.NotFound, "Area id not found", null);
            }
            var user = new User()
            {
                Name = distributorModel.Name,
                Email = distributorModel.Email,
                PhoneNumber = distributorModel.PhoneNumber,
                Area = area,
                AreaId = areaId,
                Distributor = new Distributor()
                {
                    Address = distributorModel.Address,
                }
            };
            context.Users.Add(user);
            await context.SaveChangesAsync();
            return new ResponseModel(System.Net.HttpStatusCode.OK, "Success", user.Id);
        }

        public async Task<ResponseModel> addUserToAreaAsync(UserModel userModel)
        {
            var role = await context.Roles.FirstOrDefaultAsync(x => x.Id == userModel.RoleId);
            if (role == null)
            {
                return new ResponseModel(System.Net.HttpStatusCode.NotFound, "Role id not found", null);
            }
            var area = await context.Area.FirstOrDefaultAsync(x => x.Id == userModel.AreaId);
            if (area == null)
            {
                return new ResponseModel(System.Net.HttpStatusCode.NotFound, "Area id not found", null);
            }
            var user = new User()
            {
                Name = userModel.Name,
                Email = userModel.Email,
                InUser = new InternalUser()
                {
                    Role = role,
                    RoleId = userModel.RoleId,
                    Staff = new Staff()
                    {
                        IsActived = userModel.IsActived
                    }
                },
                Area = area,
                AreaId = userModel.AreaId,
            };
            context.Users.Add(user);
            await context.SaveChangesAsync();
            return new ResponseModel(System.Net.HttpStatusCode.OK, "Success", user.Id);
        }

        public async Task<ResponseModel> assignUsersToAreaAsync(int areaId, List<int> userIds)
        {
            var area = await context.Area.FirstOrDefaultAsync(x => x.Id == areaId);
            if (area == null)
            {
                return new ResponseModel(System.Net.HttpStatusCode.NotFound, "Area Id not found", null);
            }
            var usersOfArea = area.Users;
            var userIdsOfArea = new List<int>();
            foreach (var user in usersOfArea)
            {
                userIdsOfArea.Add(user.Id);
            }
            var userIdsAssigned = userIds.Except(userIdsOfArea).ToList();

            foreach (var id in userIdsAssigned)
            {
                var user = await context.Users.FirstOrDefaultAsync(x => x.Id == id);
                if (user != null)
                {
                    usersOfArea.Add(user);
                }
            }
            await context.SaveChangesAsync();
            return new ResponseModel(System.Net.HttpStatusCode.OK, "Success", null);
        }

        public async Task<ResponseModel> createAreaAsync(AreaModel areaModel)
        {
            var areas = await context.Area.ToListAsync();
            var lastAreaId = areas[areas.Count - 1].Id;
            var area = new Area()
            {
                Name = areaModel.Name,
                Code = "COD" + lastAreaId
            };
            context.Area.Add(area);
            await context.SaveChangesAsync();
            return new ResponseModel(System.Net.HttpStatusCode.OK, "Success", area.Id);
        }

        public async Task<ResponseModel> getAreaByIdAsync(int id)
        {
            var area = await context.Area.FirstOrDefaultAsync(x => x.Id == id);
            if (area == null)
            {
                return new ResponseModel(System.Net.HttpStatusCode.NotFound, "Area Id not found", null);
            }
            var users = area.Users;
            var userIds = new List<int>();
            foreach (var user in users)
            {
                userIds.Add(user.Id);
            }
            return new ResponseModel(System.Net.HttpStatusCode.OK, "Success", new AreaModel()
            {
                Name = area.Name,
                Code = area.Code,
                UserIds = userIds
            });
        }

        public async Task<ResponseModel> getAreasAsync()
        {
            var areas = await context.Area.ToListAsync();
            var areaModels = new List<AreaModel>();
            foreach (var area in areas)
            {
                var users = area.Users;
                var userIds = new List<int>();
                foreach (var user in users)
                {
                    userIds.Add(user.Id);
                }
                areaModels.Add(new AreaModel()
                {
                    Code = area.Code,
                    Name = area.Name,
                    UserIds = userIds
                });
            }
            return new ResponseModel(System.Net.HttpStatusCode.OK, "Success", areaModels);
        }

        public async Task<ResponseModel> getDistributorsByAreaIdAsync(int areaId)
        {
            var area = await context.Area.FirstOrDefaultAsync(x => x.Id == areaId);
            if (area == null)
            {
                return new ResponseModel(System.Net.HttpStatusCode.NotFound, "Area Id not found", null);
            }
            var users = area.Users;
            var distributorModels = new List<DistributorModel>();
            foreach (var u in users)
            {
                if (u.Distributor != null)
                {
                    distributorModels.Add(new DistributorModel()
                    {
                        Name = u.Name,
                        Email = u.Email,
                        Address = u.Distributor.Address,
                        PhoneNumber = u.PhoneNumber
                    });
                }
            }
            return new ResponseModel(System.Net.HttpStatusCode.OK, "Success", distributorModels);
        }

        public async Task<ResponseModel> updateAreaAsync(int areaId, AreaModel areaModel)
        {
            var area = await context.Area.FirstOrDefaultAsync(x => x.Id == areaId);
            if (area == null)
            {
                return new ResponseModel(System.Net.HttpStatusCode.NotFound, "Area Id not found", null);
            }
            area.Name = areaModel.Name;
            area.Code = areaModel.Code;
            return new ResponseModel(System.Net.HttpStatusCode.OK, "Success", null);
        }
    }
}
