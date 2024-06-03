using BArtTask.Contracts;
using BArtTask.Data;
using BArtTask.Models;
using Microsoft.EntityFrameworkCore;

namespace BArtTask.Services
{
    public class ContactService : IContactService
    {
        private readonly AppDbContext _context;

        public ContactService(AppDbContext context)
        {
            _context = context;
        }

        public async Task Create(ContactRequest request)
        {
            var contact = await _context.Contacts
                .Include(c => c.Accounts)
                .FirstOrDefaultAsync(c => c.Email == request.Email);

            if (contact == null)
            {
                contact = new Contact
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    Accounts = new List<Account>()
                };

                _context.Contacts.Add(contact);
            }
            await _context.SaveChangesAsync();
        }
    }
}
