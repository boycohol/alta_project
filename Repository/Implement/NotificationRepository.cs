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
    }
}
