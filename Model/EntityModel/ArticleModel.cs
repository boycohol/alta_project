

namespace AltaProject.Model.EntityModel
{
    public class ArticleModel
    {
        public string Title { get; set; }
        public string HyperText { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string CreatedDate { get; set; }
        public int CreatorUserId { get; set; }
        public int FileId { get; set; }
    }
}
