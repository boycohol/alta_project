namespace AltaProject.Model.EntityModel
{
    public class VisitPlanModel
    {
        public string Date { get; set; }
        public int TimeId { get; set; }
        public string Status { get; set; }
        public int DistributorId { get; set; }
        public string Purpose { get; set; }
        public List<int> GuestIds { get; set; }
    }
}
