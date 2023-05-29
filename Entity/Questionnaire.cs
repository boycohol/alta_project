using System.ComponentModel.DataAnnotations;

namespace AltaProject.Entity
{
    public class Questionnaire
    {
        [Key]
        public int Id { get; set; }
        public bool isAvailabe { get; set; }
        public string QuestionText { get; set; }
        public List<string> Answers { get; set; }
        public virtual ICollection<Survey> Surveys { get; set; }
    }
}
