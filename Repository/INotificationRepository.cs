using AltaProject.Model.EntityModel;
using AltaProject.Response;

namespace AltaProject.Repository
{
    public interface INotificationRepository
    {
        public Task<ResponseModel> sendNotificationAsync(NotificationModel notificationModel);
        public Task<ResponseModel> sendNotificationBeforeEventStarted();
    }
}
