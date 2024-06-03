using BArtTask.Models;

namespace BArtTask.Contracts
{
    public class AccountRequest
    {
        public string Name { get; set; }
        public string ContactFirstName { get; set; }
        public string ContactLastName { get; set; }
        public string ContactEmail { get; set; }
    }
}
