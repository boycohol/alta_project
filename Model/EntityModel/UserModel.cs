using System.ComponentModel.DataAnnotations;

namespace AltaProject.Model.EntityModel
{
    public class UserModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
        public int AreaId { get; set; }
        public int ReportedUserId { get; set; }
        public bool IsActived { get; set; }
    }
}
