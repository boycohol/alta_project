using AltaProject.Data;
using AltaProject.Entity;
using AltaProject.Model.EntityModel;
using AltaProject.Response;
using Microsoft.EntityFrameworkCore;

namespace AltaProject.Repository.Implement
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly ApplicationDBContext context;
        public NotificationRepository(ApplicationDBContext context)
        {
            this.context = context;
        }

        public async Task<ResponseModel> sendNotificationAsync(NotificationModel notificationModel)
        {
            var receiver = await context.Users.FirstOrDefaultAsync(x => x.Id == notificationModel.UserReceiverId);
            var sender = await context.InternalUsers.FirstOrDefaultAsync(x => x.Id == notificationModel.SenderUserId);
            if (receiver != null)
            {
                var notifi = new Notification()
                {
                    SenderUser = sender,
                    SenderUserId = notificationModel.SenderUserId,
                    Title = notificationModel.Title,
                    Detail = notificationModel.Detail,
                    UserReceiver = receiver,
                    UserReceiverId = notificationModel.UserReceiverId
                };
                context.Notifications.Add(notifi);
            }

            await context.SaveChangesAsync();
            return new ResponseModel(System.Net.HttpStatusCode.OK, "Success", null);
        }

        public async Task<ResponseModel> sendNotificationBeforeEventStarted()
        {
            var dateTomorow = DateTime.UtcNow.AddDays(1);
            var plans = await context.VisitPlans.Where(x => x.Date == dateTomorow || x.Date < dateTomorow).ToListAsync();
            var tasks = await context.Tasks.Where(x => x.StartDate == dateTomorow || x.StartDate < dateTomorow).ToListAsync();
            foreach (var plan in plans)
            {
                var notifyDistributor = new NotificationModel()
                {
                    Title = "There is a visit plan will start soon",
                    Detail = "There is a visit plan will start soon",
                    SenderUserId = 0,
                    UserReceiverId = plan.DistributorId
                };
                var responseSendDistributor = await sendNotificationAsync(notifyDistributor);
                if (responseSendDistributor.code != System.Net.HttpStatusCode.OK)
                {
                    return responseSendDistributor;
                }
                foreach (var guest in plan.Guests)
                {
                    var notifyGuest = new NotificationModel()
                    {
                        Title = "There is a visit plan will start soon",
                        Detail = "There is a visit plan will start soon",
                        SenderUserId = 0,
                        UserReceiverId = guest.Id,
                    };
                    var responseSendGuest = await sendNotificationAsync(notifyGuest);
                    if (responseSendGuest.code != System.Net.HttpStatusCode.OK)
                    {
                        return responseSendGuest;
                    }
                }
            }
            foreach (var task in tasks)
            {
                if (task.AssigneeStaffId != null)
                {
                    var notifyAssignee = new NotificationModel()
                    {
                        Title = "There is a task will start soon",
                        Detail = "There is a task will start soon",
                        SenderUserId = 0,
                        UserReceiverId = (int)task.AssigneeStaffId
                    };
                    var responseSendAssignee = await sendNotificationAsync(notifyAssignee);
                    if (responseSendAssignee.code != System.Net.HttpStatusCode.OK)
                    {
                        return responseSendAssignee;
                    }
                }
                else
                {
                    var notifyCreator = new NotificationModel()
                    {
                        Title = "There is a task will start soon",
                        Detail = "There is a task will start soon, but no one has this task",
                        SenderUserId = 0,
                        UserReceiverId = task.CreatorUserId
                    };
                    var responseSendCreator = await sendNotificationAsync(notifyCreator);
                    if (responseSendCreator.code != System.Net.HttpStatusCode.OK)
                    {
                        return responseSendCreator;
                    }
                }
            }
            return new ResponseModel(System.Net.HttpStatusCode.OK, "Success", null);
        }
    }
    //Check SOLID between notificationRepo and visitplanRepo
    //test api
    //DistributorRepo ? should have or not?
}
