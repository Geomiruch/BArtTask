using BArtTask.Contracts;
using BArtTask.Data;
using BArtTask.Models;
using Microsoft.EntityFrameworkCore;

namespace BArtTask.Services
{
    public class IncidentService : IIncidentService
    {
        private readonly AppDbContext _context;

        public IncidentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task Create(IncidentRequest request)
        {
            var account = await _context.Accounts
                .Include(a => a.Contact)
                .FirstOrDefaultAsync(a => a.Name == request.AccountName);

            if (account == null)
            {
                throw new KeyNotFoundException("Account not found.");
            }

            var contact = await _context.Contacts
                .Include(c => c.Accounts)
                .FirstOrDefaultAsync(c => c.Email == request.ContactEmail);

            if (contact == null)
            {
                contact = new Contact
                {
                    FirstName = request.ContactFirstName,
                    LastName = request.ContactLastName,
                    Email = request.ContactEmail,
                    Accounts = new List<Account> { account }
                };

                _context.Contacts.Add(contact);

                var new_incident = new Incident
                {
                    Description = "Contact not found",
                    Account = account,
                };

                _context.Incidents.Add(new_incident);
            }
            else
            {
                if (!contact.Accounts.Any(a => a.Id == account.Id))
                {
                    contact.Accounts.Add(account);
                }
            }

            var incident = new Incident
            {
                Description = request.IncidentDescription,
                Account = account
            };

            _context.Incidents.Add(incident);
            await _context.SaveChangesAsync();
        }
    }
}
