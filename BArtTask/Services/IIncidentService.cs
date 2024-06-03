using BArtTask.Contracts;
using BArtTask.Models;

namespace BArtTask.Services
{
    public interface IIncidentService
    {
        public Task Create(IncidentRequest request);
    }
}
