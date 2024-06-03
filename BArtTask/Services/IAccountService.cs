using BArtTask.Contracts;

namespace BArtTask.Services
{
    public interface IAccountService
    {
        public Task Create(AccountRequest request);
    }
}
