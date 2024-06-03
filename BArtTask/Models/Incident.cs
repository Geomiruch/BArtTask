using System.ComponentModel.DataAnnotations;

namespace BArtTask.Models
{
    public class Incident
    {
        [Key]
        public string Name { get; set; }
        public string Description { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }
    }
}
