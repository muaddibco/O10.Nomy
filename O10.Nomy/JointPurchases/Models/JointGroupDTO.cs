namespace O10.Nomy.JointPurchases.Models
{
    public class JointGroupDTO
    {
        public long JointGroupId { get; set; }
        public long O10RegistrationId { get; set; }

        public long O10GroupId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
