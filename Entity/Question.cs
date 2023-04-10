using System.ComponentModel.DataAnnotations;

namespace AltaProject.Entity
{
    public class Question
    {
        [Key]
        public int Id { get; set; }
        public string QuestionText { get; set; }

        public int SurveyId { get; set; }
        public virtual Survey Survey { get; set; }
    }
}
