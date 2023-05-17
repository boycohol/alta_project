namespace AltaProject.Model.EntityModel
{
    public class TaskModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string StartDate { get; set; }
        public string? EndDate { get; set; }
        public string Category { get; set; }
        public float? Rating { get; set; }
        public int CreatorUserId { get; set; }
        public int AssigneeStaffId { get; set; }
        public List<string>? Files { get; set; }
        public List<string>? Comments { get; set; }
    }
}
