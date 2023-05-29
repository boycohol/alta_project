namespace AltaProject.Model.EntityModel
{
    public class SurveyModel
    {
        public string Title { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int CreatorUserId { get; set; }
        public int QuestionnaireId { get; set; }
        public List<int> ImplementUserIds { get; set; }
    }
}
