using BArtTask.Contracts;
using BArtTask.Data;
using BArtTask.Models;
using Microsoft.EntityFrameworkCore;

namespace BArtTask.Services
{
    public class AccountService : IAccountService
    {
        private readonly AppDbContext _context;

        public AccountService(AppDbContext context)
        {
            _context = context;
        }
        public async Task Create(AccountRequest request)
        {
            var account = await _context.Accounts
                .Include(a => a.Contact)
                .FirstOrDefaultAsync(a => a.Name == request.Name);

            var contact = await _context.Contacts
                .Include(c => c.Accounts)
                .FirstOrDefaultAsync(c => c.Email == request.ContactEmail);

            if (account == null)
            {
                if (contact == null)
                {
                    contact = new Contact
                    {
                        FirstName = request.ContactFirstName,
                        LastName = request.ContactLastName,
                        Email = request.ContactEmail,
                        Accounts = new List<Account>()
                    };

                    _context.Contacts.Add(contact);
                }

                account = new Account
                {
                    Name = request.Name,
                    Contact = contact,
                    Incidents = new List<Incident>()
                };

                _context.Accounts.Add(account);
            }
            else
            {
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
                }
                else
                {
                    if (!contact.Accounts.Any(a => a.Id == account.Id))
                    {
                        contact.Accounts.Add(account);
                    }
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
