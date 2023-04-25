namespace AltaProject.Model.EntityModel
{
    public class StaffModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
        public int AreaId { get; set; }
        public bool isActived { get; set; }
        public string? Reporter { get; set; }
    }
}
