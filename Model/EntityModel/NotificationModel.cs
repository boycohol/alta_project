using AltaProject.Entity;

namespace AltaProject.Model.EntityModel
{
    public class NotificationModel
    {

        public string Title { get; set; }
        public string Detail { get; set; }

        public int SenderUserId { get; set; }
        public int UserReceiverId { get; set; }
    }
}
