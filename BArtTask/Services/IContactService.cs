using BArtTask.Contracts;

namespace BArtTask.Services
{
    public interface IContactService
    {
        public Task Create(ContactRequest request);
    }
}
