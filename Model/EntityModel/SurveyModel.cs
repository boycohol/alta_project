namespace AltaProject.Model.EntityModel
{
    public class SurveyModel
    {
        public string Title { get; set; }
        public string Status { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<int> ImplementUserIds { get; set; }
    }
}
