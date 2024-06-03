namespace BArtTask.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ContactId { get; set; }
        public Contact Contact { get; set; }
        public ICollection<Incident> Incidents { get; set; }
    }
}
