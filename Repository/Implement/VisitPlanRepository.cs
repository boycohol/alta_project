using AltaProject.Data;
using AltaProject.Entity;
using AltaProject.Model.EntityModel;
using AltaProject.Response;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MimeKit.Cryptography;
using Org.BouncyCastle.Pkcs;

namespace AltaProject.Repository.Implement
{
    public class VisitPlanRepository : IVisitPlanRepository
    {
        private readonly ApplicationDBContext context;
        private readonly IMapper mapper;
        private readonly INotificationRepository notificationRepository;

        public VisitPlanRepository(ApplicationDBContext context, IMapper mapper, INotificationRepository notificationRepository)
        {
            this.context = context;
            this.mapper = mapper;
            this.notificationRepository = notificationRepository;
        }
        private List<VisitPlanModel> transferVisitPlantoModel(List<VisitPlan> plans)
        {
            var planModels = new List<VisitPlanModel>();
            foreach (var plan in plans)
            {
                var guestIds = new List<int>();
                foreach (var g in plan.Guests)
                {
                    guestIds.Add(g.Id);
                }
                planModels.Add(new VisitPlanModel()
                {
                    Date = plan.Date.ToShortDateString(),
                    TimeId = plan.TimeId,
                    Purpose = plan.Purpose,
                    Status = plan.Status,
                    DistributorId = plan.DistributorId,
                    GuestIds = guestIds
                });

            }

            return planModels;
        }
        private bool isUserIdInThere(ICollection<Guest> guests, int userId)
        {
            foreach (var g in guests)
            {
                if (g.Id == userId) return true;
            }
            return false;
        }
        private bool isGuestBusy(Guest guest, string date)
        {
            foreach (var plan in guest.VisitPlans)
            {
                DateTime dateTime = DateTime.Parse(date).ToUniversalTime();
                if (dateTime == plan.Date) return true;
            }
            return false;
        }
        public async Task<ResponseModel> createVisitPlanAsync(int userId, VisitPlanModel planModel)
        {
            var user = await context.InternalUsers.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                return new ResponseModel(System.Net.HttpStatusCode.BadRequest, "User id is not found", null);
            }
            var distributor = await context.Distributors.FirstOrDefaultAsync(d => d.Id == planModel.DistributorId);
            if (distributor == null)
            {
                return new ResponseModel(System.Net.HttpStatusCode.BadRequest, "Distributor id is not found", null);
            }
            var busyGuestIds = new List<int>();

            foreach (var id in planModel.GuestIds)
            {
                var guest = await context.Guests.FirstOrDefaultAsync(g => g.Id == id);
                if (guest != null)
                {
                    /*guestList.Add(guest);*/
                    if (isGuestBusy(guest, planModel.Date))
                    {
                        busyGuestIds.Add(id);
                        continue;
                    }
                    var notifi = new NotificationModel()
                    {
                        Title = "Visit Plan Notification",
                        Detail = "You're guest for this visit plan",
                        SenderUserId = 0,
                        UserReceiverId = id
                    };
                    await notificationRepository.sendNotificationAsync(notifi);
                }
            }

            var plan = new VisitPlan()
            {
                Date = DateTime.Parse(planModel.Date).ToUniversalTime(),
                Status = "NotDone",
                Purpose = planModel.Purpose,
                TimeId = planModel.TimeId,
                RequestorUser = user,
                DistributorId = planModel.DistributorId,
                Distributor = distributor,
                Guests = new List<Guest>()
            };
            context.VisitPlans.Add(plan);
            await context.SaveChangesAsync();
            return new ResponseModel(System.Net.HttpStatusCode.OK, "Send visit plan to guest, wait to response", null);
        }

        public async Task<ResponseModel> deleteVisitPlanAsync(int visitPlanId)
        {
            var plan = await context.VisitPlans.FirstOrDefaultAsync(p => p.Id == visitPlanId);
            if (plan == null)
            {
                return new ResponseModel(System.Net.HttpStatusCode.BadRequest, "Visit plan Id not found", null);
            }
            context.VisitPlans.Remove(plan);
            await context.SaveChangesAsync();
            return new ResponseModel(System.Net.HttpStatusCode.OK, "Delete Success", null);
        }

        public async Task<ResponseModel> getVisitPlanByIdAsync(int visitPlanId)
        {
            var plan = await context.VisitPlans.FirstOrDefaultAsync(p => p.Id == visitPlanId);
            if (plan == null)
            {
                return new ResponseModel(System.Net.HttpStatusCode.BadRequest, "Visit plan Id not found", null);
            }
            var plans = new List<VisitPlan>() { plan };
            var planModel = transferVisitPlantoModel(plans);
            return new ResponseModel(System.Net.HttpStatusCode.OK, "Success", planModel);
        }

        public async Task<ResponseModel> getVisitPlanByUserIdAsync(int userId)
        {
            var guest = await context.Guests.FirstOrDefaultAsync(g => g.Id == userId);
            var plans = new List<VisitPlan>();
            if (guest == null)
            {
                plans = await context.VisitPlans.Where(vp => vp.RequestorUser.Id == userId)
                   .ToListAsync();
            }
            else
            {
                plans = await context.VisitPlans.Where(vp => isUserIdInThere(vp.Guests, userId))
                   .ToListAsync();
            }
            var planModels = transferVisitPlantoModel(plans);
            return new ResponseModel(System.Net.HttpStatusCode.OK, "Success!", planModels);
        }

        public async Task<ResponseModel> getVisitPlansNotStartYetAsync()
        {
            var dateNow = DateTime.UtcNow;
            var plans = await context.VisitPlans.Where(p => DateTime.Compare(dateNow, p.Date) < 0).Select(x => x.Id).ToListAsync();
            return new ResponseModel(System.Net.HttpStatusCode.OK, "Success", plans);
        }
        public async Task<ResponseModel> searchVisitPlanByInfoAsync(string information)
        {
            var plans = await context.VisitPlans.Where(p =>
            p.Date.ToString().Contains(information)
            || p.Purpose.Contains(information)
            || p.Distributor.User.Name.Contains(information)).ToListAsync();
            var planModels = transferVisitPlantoModel(plans);
            //or search guest info... after
            return new ResponseModel(System.Net.HttpStatusCode.OK, "Success", planModels);
        }
        public async Task<ResponseModel> updateVisitPlanAsync(int visitPlanId, VisitPlanModel planModel)
        {
            var plan = await context.VisitPlans.FirstOrDefaultAsync(p => p.Id == visitPlanId);
            if (plan == null)
            {
                return new ResponseModel(System.Net.HttpStatusCode.BadRequest, "Visit plan Id not found", null);
            }
            //Find New Distributor
            var distributor = await context.Distributors.FirstOrDefaultAsync(d => d.Id == planModel.DistributorId);
            if (distributor == null)
            {
                return new ResponseModel(System.Net.HttpStatusCode.BadRequest, "Distributor Id not found", null);
            }
            //Find Guest
            var guests = plan.Guests;
            var guestIds = new List<int>();
            foreach (var g in guests)
            {
                guestIds.Add(g.Id);
            }
            var guestModelIds = planModel.GuestIds;
            //Find different guests
            var oldGuests = guestIds.Except(guestModelIds).ToList();
            var newGuests = guestModelIds.Except(guestIds).ToList();

            //Now update
            plan.Status = planModel.Status;
            plan.Date = DateTime.Parse(planModel.Date);
            plan.Purpose = planModel.Purpose;
            plan.Distributor = distributor;
            foreach (var g in oldGuests)
            {
                var removeGuest = guests.FirstOrDefault(x => x.Id == g);
                if (removeGuest == null)
                {
                    return new ResponseModel(System.Net.HttpStatusCode.InternalServerError, "Something went wrong", null);
                }
                guests.Remove(removeGuest);
            }
            foreach (var g in newGuests)
            {
                var addGuest = await context.Guests.FirstOrDefaultAsync(x => x.Id == g);
                if (addGuest == null)
                {
                    return new ResponseModel(System.Net.HttpStatusCode.InternalServerError, "Something went wrong", null);
                }
                guests.Add(addGuest);
            }
            await context.SaveChangesAsync();
            return new ResponseModel(System.Net.HttpStatusCode.OK, "Success", null);
        }

        public async Task<ResponseModel> acceptVisitInvitationAsync(int userId, int visitPlanId)
        {
            var plan = await context.VisitPlans.FirstOrDefaultAsync(x => x.Id == visitPlanId);
            if (plan == null)
            {
                return new ResponseModel(System.Net.HttpStatusCode.NotFound, "Visit Plan Id not found", null);
            }
            var guest = await context.Guests.FirstOrDefaultAsync(x => x.User.Id == userId);
            if (guest == null)
            {
                return new ResponseModel(System.Net.HttpStatusCode.NotFound, "User Id not found", null);
            }
            plan.Guests.Add(guest);
            await context.SaveChangesAsync();
            return new ResponseModel(System.Net.HttpStatusCode.OK, "Success", null);
        }
    }
}
