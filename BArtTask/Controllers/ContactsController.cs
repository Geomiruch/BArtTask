using BArtTask.Contracts;
using BArtTask.Data;
using BArtTask.Models;
using BArtTask.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BArtTask.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactsController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateContact([FromBody] ContactRequest request)
        {
            await _contactService.Create(request);
            return Ok();
        }
    }
}
